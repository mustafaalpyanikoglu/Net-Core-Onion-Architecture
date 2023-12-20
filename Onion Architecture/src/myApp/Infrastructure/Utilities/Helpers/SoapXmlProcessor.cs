using Application.Features.LocationSolvers.Dtos;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using static Infrastructure.Constants.SoapXmlProcessorConstants;

namespace Infrastructure.Utilities.Helpers;

public class SoapXmlProcessor
{
    public static string SerializeToXml<T>(T objectToSerialize)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StringBuilder xmlStringBuilder = new StringBuilder();

        using (XmlWriter xmlWriter = XmlWriter.Create(xmlStringBuilder, new XmlWriterSettings { OmitXmlDeclaration = false }))
        {
            xmlWriter.WriteStartElement(SOAP12, ENVELOPE, XMLNSSOAP12);
            xmlWriter.WriteAttributeString(XMLNS, XSI, null, XML_SCHEMA_INSTANCE);
            xmlWriter.WriteAttributeString(XMLNS, XSD, null, XML_SCHEMA);
            serializer.Serialize(xmlWriter, objectToSerialize);
            xmlWriter.WriteEndElement();
        }

        return xmlStringBuilder.ToString();
    }

    public static LocationSolverResult DeserializeSoapResponse(string responseContent)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LocationSolverResponseEnvelope));

        using (StringReader reader = new StringReader(responseContent))
        {
            LocationSolverResponseEnvelope responseModel = (LocationSolverResponseEnvelope)serializer.Deserialize(reader);
            return responseModel.Body.LocationSolverResponse.LocationSolverResult;
        }
    }
}
