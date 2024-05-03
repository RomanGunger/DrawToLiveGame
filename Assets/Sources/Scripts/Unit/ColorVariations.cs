using UnityEngine;

[CreateAssetMenu(fileName = "ColorPaletes", menuName = "ColorPaletes/New Color Paletes", order = 2)]
public class ColorVariations : ScriptableObject
{
    [SerializeField] ColorPaleteClass[] colorPalete = null;

    public Color GetColorR(int index)
    {
        return colorPalete[index].colorR;
    }

    public Color GetColorG(int index)
    {
        return colorPalete[index].colorG;
    }

    public Color GetColorB(int index)
    {
        return colorPalete[index].colorB;
    }

    public Color GetColorLights(int index)
    {
        return colorPalete[index].colorLights;
    }

    public int GetPaletesCount()
    {
        return colorPalete.Length;
    }

    [System.Serializable]
    class ColorPaleteClass
    {
        public Color colorR;
        public Color colorG;
        public Color colorB;
        public Color colorLights;
    }
}


