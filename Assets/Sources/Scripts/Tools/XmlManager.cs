using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class XmlManager
{
    public void Save(SaveFile saveFile)
    {
        saveFile._chapters.Clear();
        saveFile._levels.Clear();

        foreach (var kvp in saveFile._passedLevels)
        {
            saveFile._chapters.Add(kvp.Key);
            saveFile._levels.Add(kvp.Value);
        }

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
                SaveFile saveFile = (SaveFile?)serializer.Deserialize(reader);

                for (int i = 0; i < saveFile._chapters.Count ; i++)
                {
                    saveFile._passedLevels.Add(saveFile._chapters[i], saveFile._levels[i]);
                }

                return saveFile;
            }
        }
        else
        {
            return CreateNewXML();
        }
    }

    SaveFile? CreateNewXML()
    {
        SaveFile saveFile = new SaveFile();
        saveFile._passedLevels.Clear();
        saveFile._passedLevels.Add(0, new List<int> { });
        Save(saveFile);
        return saveFile;
    }

    public void RemoveXML()
    {
        File.Delete(Application.persistentDataPath + "/saveFile.xml");
    }
}
