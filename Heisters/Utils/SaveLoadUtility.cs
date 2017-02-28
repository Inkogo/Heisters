using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.IO;

namespace Heisters
{
    class SaveLoadUtility
    {
        JavaScriptSerializer serializer;

        public string DataPath
        {
            get
            {
                return "";
            }
        }

        public SaveLoadUtility()
        {
            serializer = new JavaScriptSerializer();
            InjectionContainer.Instance.RegisterObject(this);
        }

        public void Serialize<T>(T t, string path)
        {
            File.WriteAllText(path, serializer.Serialize(t));
        }

        public T Deserialize<T>(string path)
        {
            return serializer.Deserialize<T>(File.ReadAllText(path));
        }
    }
}
