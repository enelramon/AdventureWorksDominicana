using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AdventureWorksDominicana.Blazor.Helpers
{
    public static class XmlHelper
    {
        public static string? SerializarAXml<T>(T objeto) where T : class
        {
            if (objeto == null) return null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));

                // IMPORTANTE: Esto evita que al XML se le ponga arriba la típica línea "<?xml version="1.0"?>"
                // SQL Server de AdventureWorks muchas veces la rechaza si está definida como Schema.
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false };

                var namespaces = new XmlSerializerNamespaces();
                var xmlRoot = (XmlRootAttribute?)Attribute.GetCustomAttribute(typeof(T), typeof(XmlRootAttribute));

                // Si la clase tiene Namespace (como lo hicimos en el Paso 1), lo aplicamos al documento limpio.
                if (xmlRoot != null && !string.IsNullOrWhiteSpace(xmlRoot.Namespace))
                    namespaces.Add("", xmlRoot.Namespace);
                else
                    namespaces.Add("", "");

                using var sw = new StringWriter();
                using var writer = XmlWriter.Create(sw, settings);
                serializer.Serialize(writer, objeto, namespaces); // Convertimos nuestro objeto!
                return sw.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static T? DeserializarDesdeXml<T>(string? xml) where T : class
        {
            if (string.IsNullOrWhiteSpace(xml)) return null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using var reader = new StringReader(xml);
                return (T?)serializer.Deserialize(reader); // Pasamos del texto monstruoso a nuestro cómodo DTO
            }
            catch
            {
                return null;
            }
        }
    }
}