using System.Xml.Serialization;

namespace Application.Features.LocationSolvers.Dtos;

[Serializable]
[XmlRoot(ElementName = "CustomerWLPDtos")]
public class CustomerWLPDtos
{
    [XmlElement(ElementName = "CustomerWLPDto")]
    public List<CustomerWLPDto> CustomerWLPDto { get; set; }
}
[XmlRoot(ElementName = "CustomerWLPDto")]
public class CustomerWLPDto
{
    [XmlElement(ElementName = "Id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "UserID")]
    public int UserID { get; set; }

    [XmlElement(ElementName = "Demand")]
    public int Demand { get; set; }

    [XmlElement(ElementName = "CustomerWarehouseCostWlpDtos")]
    public ListCustomerWarehouseCosts CustomerWarehouseCostWlpDtos { get; set; }
}

[Serializable]
[XmlRoot(ElementName = "CustomerWarehouseCostWlpDto")]
public class CustomerWarehouseCostWlpDto
{
    [XmlElement("Id")]
    public int Id { get; set; }

    [XmlElement("CustomerId")]
    public int CustomerId { get; set; }

    [XmlElement("WarehouseID")]
    public int WarehouseID { get; set; }

    [XmlElement("Cost")]
    public double Cost { get; set; }
}

public class ListCustomerWarehouseCosts
{
    [XmlElement(ElementName = "CustomerWarehouseCostWlpDto")]
    public List<CustomerWarehouseCostWlpDto> CustomerWarehouseCostWlpDto { get; set; }
}
