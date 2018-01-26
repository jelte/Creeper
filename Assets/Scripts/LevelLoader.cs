using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ColorToPrefab {

    public Color32 color;
    public GameObject prefab;


}



public class LevelLoader : MonoBehaviour {


    public Texture2D LevelMap;
    public ColorToPrefab[] colorToPrefab;


	// Use this for initialization
	void Start () {

        LoadMap();

        /*
        Color32  c32 = new Color32(255, 0, 0, 255); //red

        Color32[] allMyPixels32 = LevelMap.GetPixels32();
        allMyPixels32[4] = c32;
        LevelMap.SetPixels32(allMyPixels32);
        */

    }
	
    public void EmptyMap()
    {
        //find all children and destroy them all.

        while(transform.childCount > 0)
        {
            Transform c = transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }
    }

    public void LoadMap()
    {
        EmptyMap();
        //get pixels from leve image map

        Color32[] allPixels = LevelMap.GetPixels32();
        int width = LevelMap.width;
        int height = LevelMap.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SpawnTileAt(allPixels[(y * width) + x], x, y);

            }
        }
    }

    void SpawnTileAt(Color32 c , int x, int y)
    {
        //transparent tile
        if (c.a == 0)
        {
            return;
        }

        //find right color

        foreach(ColorToPrefab ctp in colorToPrefab)
        {
            //if(ctp.color.r == c.r && ctp.color.b == c.b && ctp.color.g == c.g && ctp.color.a == c.a)
            if (c.Equals(ctp.color))
            {
                GameObject go = Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity);
                return;
            }
        }


        Debug.Log("No color found");
    }

}
