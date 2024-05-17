using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using DG.Tweening.Plugins.Core.PathCore;
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

        //saveFile._level.Clear();
        //saveFile._stars.Clear();

        //foreach (var kvp in saveFile._starsLevel)
        //{
        //    saveFile._level.Add(kvp.Key);
        //    saveFile._stars.Add(kvp.Value);
        //}

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

                for (int i = 0; i != Math.Min(saveFile._chapters.Count, saveFile._levels.Count); i++)
                    saveFile._passedLevels.Add(saveFile._chapters[i], saveFile._levels[i]);

                //for (int i = 0; i != Math.Min(saveFile._level.Count, saveFile._stars.Count); i++)
                //    saveFile._passedLevels.Add(saveFile._level[i], saveFile._stars[i]);

                return saveFile;
            }
        }
        else
        {
            return new SaveFile();
        }
    }

    public void RemoveXML()
    {
        File.Delete(Application.persistentDataPath + "/saveFile.xml");
    }
}
