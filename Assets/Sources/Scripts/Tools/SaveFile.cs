using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine.UIElements;

public class SaveFile
{
    public int _level = 0;
    public int _currentChapter = 0;
    public bool _settingsSoundToogle = true;
    public bool _settingsmusicToogle = true;
    public List<int> _chapters = new List<int>();
    public List<int> _levels = new List<int>();
    public int currency = 0;
    public ulong _lastTimeFreeLiveReceive = 0;
    public int _lives;
    public int _maxLives = 5;

    public bool _soundON = true;
    public bool _musicON = true;

    [XmlIgnore]
    public Dictionary<int, int> _passedLevels = new Dictionary<int, int>();
}
