using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFTP.UI
{
    public class AlphaSync : MonoBehaviour
    {
        private Graphic graphic;
        private float alpha;

        // Use this for initialization
        void Start()
        {
            graphic = GetComponent<Graphic>();
            SyncAlpha();
        }

        // Update is called once per frame
        void Update()
        {
            SyncAlpha();
        }

        void SyncAlpha()
        {
            if (this.alpha != graphic.color.a)
            {
                this.alpha = graphic.color.a;
                foreach (Graphic g in GetComponentsInChildren<Graphic>())
                {
                    g.color = new Color(
                        g.color.r,
                        g.color.g,
                        g.color.b,
                        this.alpha
                    );
                }
            }
        }
    }
}