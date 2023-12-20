using System.Xml.Serialization;

namespace Application.Features.LocationSolvers.Dtos
{

    [Serializable]
    [XmlRoot(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Body
    {
        [XmlElement(ElementName = "LocationSolver", Namespace = "http://tempuri.org/")]
        public LocationSolver LocationSolver { get; set; }
    }
}