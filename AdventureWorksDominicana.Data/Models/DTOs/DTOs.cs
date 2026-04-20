using System.Xml.Serialization;

namespace AdventureWorksDominicana.Data.Models.DTOs
{
    // --- DTO para Store Demographics ---
    [XmlRoot(ElementName = "StoreSurvey", Namespace = "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/StoreSurvey")]
    public class StoreDemographicsDto
    {
        [XmlElement]
        public decimal? AnnualSales { get; set; }

        [XmlElement]
        public string? BankName { get; set; }

        [XmlElement]
        public int? YearOpened { get; set; }

        [XmlElement]
        public int? NumberEmployees { get; set; }
    }

    // --- DTOs para Product Model ---
    [XmlRoot(ElementName = "ProductDescription", Namespace = "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription")]
    public class CatalogDto
    {
        [XmlElement]
        public string? Summary { get; set; }
    }

    // ✅ ESTE ES EL QUE FALTABA: el [XmlRoot] con el Namespace correcto
    [XmlRoot(ElementName = "root", Namespace = "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions")]
    public class InstructionsDto
    {
        [XmlElement(ElementName = "Location")]
        public LocationDto Location { get; set; } = new LocationDto();
    }

    public class LocationDto
    {
        [XmlAttribute(AttributeName = "LocationID")]
        public int LocationID { get; set; } = 10;

        [XmlElement(ElementName = "step")]
        public string? Step { get; set; }
    }
}