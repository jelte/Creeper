using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//written by poon


namespace ProjectFTP.Level
{
    public class LevelLoader
    {
        public void LoadLevel(Texture2D map, ImageConversionScheme conversionScheme, Transform parent)
        {
            EmptyMap(parent);

            Color32[] allPixels = map.GetPixels32();
            int width = map.width;
            int height = map.height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GameObject tile = SpawnTileAt(conversionScheme, allPixels[(y * width) + x], x, y);
                    if (tile != null)
                    {
                        tile.transform.parent = parent;
                    }
                }
            }
        }

        private void EmptyMap(Transform parent)
        {
            //find all children and destroy them all.
            while (parent.childCount > 0)
            {
                parent.GetChild(0).SetParent(null);
                GameObject.Destroy(parent.transform.GetChild(0).gameObject);
            }
        }

        private GameObject SpawnTileAt(ImageConversionScheme conversionScheme, Color32 c, int x, int y)
        {
            //transparent tile or white (do nothing)
			if (c.a == 0 || (c.r == 255 && c.b == 255 && c.g == 255))
            {
                return null;
            }

            GameObject prefab = conversionScheme.GetPrefab(c);
            if (prefab == null)
            {
                return null;
            }
            return GameObject.Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}
