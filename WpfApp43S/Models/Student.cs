using System.Xml.Serialization;

namespace WpfApp43S.Models;

public class Student
{
    [XmlAttribute]
    public int Id { get; set; }

    public string? FirstName { get; set; }

    [XmlElement(ElementName = "Last")]
    public string? LastName { get; set; }

    public int Gender { get; set; }
    public int? Age { get; set; }

    public bool ShouldSerializeAge() => Age.HasValue;
}
