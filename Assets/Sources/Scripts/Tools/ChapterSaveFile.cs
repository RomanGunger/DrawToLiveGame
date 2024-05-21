using System.Collections.Generic;
using System.Xml.Serialization;

public class ChapterSaveFile
{
    public List<int> _level = new List<int>();
    public List<int> _stars = new List<int>();

    [XmlIgnore]
    public Dictionary<int, int> _passedLevels = new Dictionary<int, int>();
}
