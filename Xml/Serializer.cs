using Serilog;
using System.Xml.Serialization;

namespace MZ_WorkerService.Xml
{
    public static class Serializer<T>
    {
        public static T DeserializeString(string xml)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                StringReader reader = new StringReader(xml);

                var model = (T)serializer.Deserialize(reader)!;

                return model;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error al deserializar XML: {XML}", xml);

                return default!;
            }
        }

        public static string SerializeToString(T model)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();

            serializer.Serialize(writer, model);

            return writer.ToString();
        }

        internal static object DeserializeString(Func<string?> toString)
        {
            throw new NotImplementedException();
        }
    }

    public static class Serializer
    {
        public static object DeserializeString(string xml, Type modelType)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(modelType);
                StringReader reader = new StringReader(xml);

                var model = serializer.Deserialize(reader);

                return model!;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error al deserializar XML: {XML}", xml);

                return default!;
            }
        }

        public static string SerializeToString(object model, Type modelType)
        {
            XmlSerializer serializer = new XmlSerializer(modelType);
            StringWriter writer = new StringWriter();

            serializer.Serialize(writer, model);

            return writer.ToString();
        }
    }
}
