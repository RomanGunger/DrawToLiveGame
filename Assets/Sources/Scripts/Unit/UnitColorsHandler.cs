using System.Collections.Generic;
using UnityEngine;

public class UnitColorsHandler : MonoBehaviour
{
    [SerializeField] ColorVariations colorVariations;
    [SerializeField] List<Renderer> renderers;
    [SerializeField] ParticleSystem lights;

    private void Start()
    {
        System.Random rnd = new System.Random();
        int randomColorPalete = rnd.Next(0, colorVariations.GetPaletesCount());

        foreach(var item in renderers)
        {
            Material[] mats = item.materials;

            mats[0].SetColor("_R_Color", colorVariations.GetColorR(randomColorPalete));
            mats[0].SetColor("_G_Color", colorVariations.GetColorG(randomColorPalete));
            mats[0].SetColor("_B_Color", colorVariations.GetColorB(randomColorPalete));

            if(lights != null)
                lights.startColor = colorVariations.GetColorLights(randomColorPalete);
        }
    }


}
