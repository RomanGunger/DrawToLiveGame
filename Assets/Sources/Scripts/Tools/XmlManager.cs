using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class XmlManager : MonoBehaviour
{
    public static void Save(SaveFile saveFile)
    {
        var serializer = new XmlSerializer(typeof(SaveFile));

        using (var writer = XmlWriter.Create(Application.dataPath + "/saveFile.xml"))
        {
            serializer.Serialize(writer, saveFile);
        }
    }

    public static SaveFile? Load()
    {
        string path = Application.dataPath + "/saveFile.xml";

        var serializer = new XmlSerializer(typeof(SaveFile));

        if (File.Exists(path))
        {
            using (var reader = XmlReader.Create(path))
            {
                return (SaveFile?)serializer.Deserialize(reader);
            }
        }
        else
        {
            return new SaveFile();
        }

    }
}
