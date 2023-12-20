using System.Xml.Serialization;

namespace Application.Features.LocationSolvers.Dtos;

[Serializable]
[XmlRoot(ElementName = "WarehouseWLPDtos")]
public class WarehouseWLPDtos
{
    [XmlElement(ElementName = "WarehouseWLPDto")]
    public List<WarehouseWLPDto> WarehouseWLPDto { get; set; }
}

[Serializable]
[XmlRoot(ElementName = "WarehouseWLPDto")]
public class WarehouseWLPDto
{
    [XmlElement(ElementName = "Id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "Capacity")]
    public int Capacity { get; set; }

    [XmlElement(ElementName = "SetupCost")]
    public double SetupCost { get; set; }
}