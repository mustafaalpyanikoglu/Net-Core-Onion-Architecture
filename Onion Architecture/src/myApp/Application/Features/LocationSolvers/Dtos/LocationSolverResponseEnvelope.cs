using System.Xml.Serialization;

namespace Application.Features.LocationSolvers.Dtos;

[XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
public class LocationSolverResponseEnvelope
{
    [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public LocationSolverResponseBody Body { get; set; }
}

public class LocationSolverResponseBody
{
    [XmlElement(ElementName = "LocationSolverResponse", Namespace = "http://tempuri.org/")]
    public LocationSolverResponse LocationSolverResponse { get; set; }
}

public class LocationSolverResponse
{
    [XmlElement(ElementName = "LocationSolverResult")]
    public LocationSolverResult LocationSolverResult { get; set; }
}

public class LocationSolverResult
{
    [XmlElement(ElementName = "Cost")]
    public double Cost { get; set; }

    [XmlElement(ElementName = "Assignments")]
    public Assignments Assignments { get; set; }
}

public class Assignments
{
    [XmlElement(ElementName = "int")]
    public List<int> IntList { get; set; }
}
