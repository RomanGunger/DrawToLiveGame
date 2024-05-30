using UnityEngine;

[CreateAssetMenu(fileName = "PopupsDatabase", menuName = "PopupsDatabase/New Popups Database", order = 3)]
public class PopupsDatabase : ScriptableObject
{
    [SerializeField] PopupDatabaseClass[] popups = null;

    public GameObject? GetPopup(string popupName)
    {
        foreach(var item in popups)
        {
            if (item.popupName == popupName)
                return item.popupObject;
        }

        return null;
    }

    [System.Serializable]
    public class PopupDatabaseClass
    {
        public string popupName;
        public GameObject popupObject;
    }
}
