using UnityEngine;
using System.Collections.Generic;

namespace ProjectFTP.Level
{
    [System.Serializable]
    public class ColorToPrefab
    {
        public Color32 color;
        public GameObject prefab;
    }

    [CreateAssetMenu(menuName ="Level/ImageConversionScheme")]
    public class ImageConversionScheme : ScriptableObject
    {
        public List<ColorToPrefab> colorsToPrefab = new List<ColorToPrefab>();

        public GameObject GetPrefab(Color32 color)
        {
            ColorToPrefab colorToPrefab = colorsToPrefab.Find(delegate (ColorToPrefab ctp)
            {
                return ctp.color.Equals(color);
            });
            if (colorToPrefab == null)
            {
                return null;
            }
            return colorToPrefab.prefab;
        }
    }
}