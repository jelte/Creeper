using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFTP.UI
{
    public class ColourSync : MonoBehaviour
    {
        private Graphic graphic;
        private Color colour;

        // Use this for initialization
        void Start()
        {
            graphic = GetComponent<Graphic>();
            SyncColour();
        }

        // Update is called once per frame
        void Update()
        {
            SyncColour();
        }

        void SyncColour()
        {
            if (this.colour != graphic.color)
            {
                this.colour = graphic.color;
                foreach (Graphic g in GetComponentsInChildren<Graphic>())
                {
                    g.color = colour;
                }
            }
        }
    }
}