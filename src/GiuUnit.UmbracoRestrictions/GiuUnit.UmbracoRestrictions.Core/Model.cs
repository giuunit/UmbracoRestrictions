namespace GiuUnit.UmbracoRestrictions.Core
{
    using System.Xml.Serialization;
   
    [XmlRoot(ElementName = "add")]
    public class Add
    {
        [XmlAttribute(AttributeName = "docTypeAlias")]
        public string DocTypeAlias { get; set; }
    }

    [XmlRoot(ElementName = "rule")]
    public class Rule
    {
        [XmlElement(ElementName = "add")]
        public Add[] AddList { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "errorMessage")]
        public string ErrorMessage { get; set; }
    }

    [XmlRoot(ElementName = "rules")]
    public class Rules
    {
        [XmlElement(ElementName = "rule")]
        public Rule[] RuleList { get; set; }
    }

    [XmlRoot(ElementName = "restrictionsConfig")]
    public class RestrictionsConfigRoot
    {
        [XmlElement(ElementName = "rules")]
        public Rules RulesNode { get; set; }
    }
}
