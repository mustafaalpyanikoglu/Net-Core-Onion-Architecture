using System.Xml.Serialization;

namespace Application.Features.LocationSolvers.Dtos;

[Serializable]
[XmlRoot(ElementName = "LocationOptimizationRequestDto")]
public class LocationOptimizationRequestDto
{
    [XmlElement(ElementName = "CustomerWLPDtos")]
    public CustomerWLPDtos CustomerWLPDtos { get; set; }

    [XmlElement(ElementName = "WarehouseWLPDtos")]
    public WarehouseWLPDtos WarehouseWLPDtos { get; set; }
}