using System.Xml.Serialization;

namespace Application.Features.LocationSolvers.Dtos;

[Serializable]
[XmlRoot(ElementName = "LocationSolver", Namespace = "http://tempuri.org/")]
public class LocationSolver
{
    [XmlElement(ElementName = "LocationOptimizationRequestDto")]
    public LocationOptimizationRequestDto LocationOptimizationRequestDto { get; set; }
}
