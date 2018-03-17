using UnityEngine;

namespace ProjectFTP.Level
{
    public class RandomBaseTile : MonoBehaviour
    {
        public Sprite[] SpriteArray;
        public int RandomTile;
        
        void Start()
        {
            this.GetComponent<SpriteRenderer>().sprite = SpriteArray[Random.Range(0, SpriteArray.Length)];
        }
    }
}
