using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Linq;

namespace TransientSDB
{
    public class SDBDesigner
    {
        #region xml names

        public const string TheSkyDatabaseX = "TheSkyDatabase";
        public const string SDBHeaderX = "TheSkyDatabaseHeader";

        public const string IdentifierX = "identifier";
        public const string SDBDescriptionX = "sdbDescription";
        //Control Data Fields
        public const string SearchPrefixX = "searchPrefix";
        public const string SpecialSDBX = "specialSDB";
        public const string PlotObjectsX = "plotObjects";
        public const string PlotLabelsX = "plotLabels";
        public const string PlotOrderX = "plotOrder";
        public const string SearchableX = "searchable";
        public const string ClickIdentifyX = "clickIdentify";
        public const string EpochX = "epoch";
        public const string ReferenceFrameX = "referenceFrame";
        public const string CrossReferenceTypeX = "crossReferenceType";
        public const string DefaultMaxFOVX = "defaultMaxFOV";
        public const string DefaultObjectTypeX = "defaultObjectType";
        //Built-in Data Fields
        public const string RAHoursX = "raHours";
        public const string RAMinutesX = "raMinutes";
        public const string RASecondsX = "raSeconds";
        public const string DecSignX = "decSign";
        public const string DecDegreesX = "decDegrees";
        public const string DecMinutesX = "decMinutes";
        public const string DecSecondsX = "decSeconds";
        public const string MagnitudeX = "magnitude";
        public const string CrossReferenceX = "crossReference";
        public const string LabelOrSearchX = "labelOrSearch";
        public const string ObjectTypeX = "objectType";
        public const string MajorAxisX = "majorAxis";
        public const string MinorAxisX = "minorAxis";
        public const string PositionAngleX = "positionAngle";
        public const string ScaleX = "scale";
        public const string DrawCommandX = "drawCommand";
        public const string ToggleGroupX = "toggleGroup";
        public const string FileNameX = "fileName";
        public const string MinimumFOVX = "minimumFOV";
        public const string MaximumFOVX = "maximumFOV";
        public const string RAMultiplierX = "raMultiplier";
        public const string SampleColumnHeaderX = "sampleColumnHeader";
        //Custom data fields
        const string UserFieldX = "userField";
        //Field attributes
        const string IndexX = "index";
        const string DescriptionX = "description";
        const string ColBegX = "colBeg";
        const string ColEndX = "colEnd";
        const string FieldIDX = "fieldID";
        #endregion

        public int DefaultObjectIndex { get; set; } = 20;
        public string DefaultObjectDescription { get; set; } = "Transient";
        public string SearchPrefix
        {
            set { ControlFields.Single(c => c.ControlName == SearchPrefixX).ControlValue = value; }
        }

        public List<ControlDesc> ControlFields = new List<ControlDesc>()
        {
            //List of values for generating TSX SDB fields
            //  that are used for general conversion to and display of the
            //  display in the "Find" function
            //
            new ControlDesc {ControlName = IdentifierX },
            new ControlDesc {ControlName = SDBDescriptionX },
            new ControlDesc {ControlName = SearchPrefixX, ControlValue = "" },
            new ControlDesc {ControlName = SpecialSDBX, ControlValue = "0" },
            new ControlDesc {ControlName = PlotObjectsX, ControlValue = "1" },
            new ControlDesc {ControlName = PlotLabelsX, ControlValue = "1" },
            new ControlDesc {ControlName = PlotOrderX, ControlValue = "2" },
            new ControlDesc {ControlName = SearchableX, ControlValue = "1" },
            new ControlDesc {ControlName = ClickIdentifyX, ControlValue = "1" },
            new ControlDesc {ControlName = EpochX, ControlValue = "2000.0" },
            new ControlDesc {ControlName = ReferenceFrameX, ControlValue = "0" },
            new ControlDesc {ControlName = CrossReferenceTypeX, ControlValue = "0" },
            new ControlDesc {ControlName = DefaultMaxFOVX, ControlValue = "360.0000" },
            new ControlDesc {ControlName = RAMultiplierX , ControlValue = "1.0" },
            new ControlDesc {ControlName = SampleColumnHeaderX, ControlValue = "" }
        };

        public List<DataColumn> DataFields = new List<DataColumn>();

        public List<DataColumn> HeaderMap = new List<DataColumn>();

        public XDocument HeaderGenerator()
        {
            XElement xHeader = new XElement(SDBHeaderX, new XAttribute("version", "1.00"));
            //Catalog Fields
            foreach (ControlDesc cf in ControlFields)
            { xHeader.Add(new XElement(cf.ControlName, cf.ControlValue)); }
            //Default Object type -- may fix this up later
            XElement dotx = new XElement(DefaultObjectTypeX,
                    new XAttribute(IndexX, DefaultObjectIndex),
                    new XAttribute(DescriptionX, DefaultObjectDescription));
            xHeader.Add(dotx);
            //Column definitions
            //Custom fields
            foreach (DataColumn dc in DataFields)
            {
                if (dc.IsBuiltIn)
                    xHeader.Add(BuiltInFieldGen(dc));
                else
                    xHeader.Add(CustomFieldGen(dc));
            }
            //Add header
            XDocument xdoc = new XDocument(
                new XDeclaration("1.0", null, null),
                new XDocumentType(TheSkyDatabaseX, null, null, null),
                xHeader);
            return xdoc;
        }

        public List<DataColumn> MakeHeaderMap(XElement sdbXML)
        {
            //the purpose of this method is to create a map of datafields to textcolumns
            //for the tns xml data file to be convert to a sdb.text file
            //Load in the header record from the tnsXML 
            XElement headX = new XElement(sdbXML.Element("SDBDataFields"));
            HeaderMap = new List<DataColumn>();
            IEnumerable<XElement> headerXList = headX.Elements();
            foreach (XElement hX in headerXList)
            {
                HeaderMap.Add(new DataColumn()
                {
                    SourceDataName = hX.Name.LocalName,
                    ColumnWidth = Convert.ToInt32(hX.Value) + 2
                });
            }
            return HeaderMap;
        }

        public XElement BuiltInFieldGen(DataColumn fc)
        {
            return new XElement(fc.TSXEntryName,
                        new XAttribute(ColBegX, fc.ColumnStart.ToString()),
                        new XAttribute(ColEndX, Utility.ColumnEnd(fc.ColumnStart, fc.ColumnWidth)));
        }

        public XElement CustomFieldGen(DataColumn uc)
        {
            return new XElement(UserFieldX,
                        new XAttribute(FieldIDX, uc.SourceDataName),
                         new XAttribute(ColBegX, uc.ColumnStart.ToString()),
                       new XAttribute(ColEndX, Utility.ColumnEnd(uc.ColumnStart, uc.ColumnWidth)));
        }

    }

    public class DataColumn
    {
        public bool IsBuiltIn = false;

        public string? TSXEntryName = null;
        public string? SourceDataName { get; set; } = null;
        public int ColumnStart { get; set; } = 0;
        public int ColumnWidth { get; set; } = 0;
        public bool IsPassed { get; set; } = false;
    }

    public class ControlDesc
    {
        public string ControlName { get; set; }
        public string ControlValue { get; set; }
    }

}

