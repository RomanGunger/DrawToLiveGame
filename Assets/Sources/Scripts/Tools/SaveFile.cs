using System.Collections.Generic;
using System.Xml.Serialization;

public class SaveFile
{
    public int _currentChapter = 0;
    public bool _settingsSoundToogle = true;
    public bool _settingsmusicToogle = true;
    public int _currency = 0;
    public ulong _lastTimeFreeLiveReceive = 0;
    public int _lives = 5;
    public int _maxLives = 5;

    public bool _adds = true;

    public bool _soundON = true;
    public bool _musicON = true;

    public List<int> _chapters = new List<int>();
    public List<List<int>> _levels = new List<List<int>>();

    [XmlIgnore]
    public Dictionary<int, List<int>> _passedLevels = new Dictionary<int, List<int>>();
}
