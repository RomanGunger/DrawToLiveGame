using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class XmlManager
{
    public void Save(SaveFile saveFile)
    {
        var serializer = new XmlSerializer(typeof(SaveFile));

        using (var writer = XmlWriter.Create(Application.persistentDataPath + "/saveFile.xml"))
        {
            serializer.Serialize(writer, saveFile);
        }
    }

    public SaveFile? Load()
    {
        string path = Application.persistentDataPath + "/saveFile.xml";

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
