using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP;


namespace ProjectFTP.Level.Traps
{
    public class DetectSpike : MonoBehaviour
    {

        public bool MoveTowards = false;
        public GameObject ResizableTrigger;
        public Vector3 tempPosition;
        public float moveSpeed;

        public float RaycastDistance = 10.0f;
        public float distanceCheck = 1.0f;

        private Vector2 PointLeft;
        private Vector2 PointRight;
        private Vector2 PointUp;
        private Vector2 PointDown;


        //raycast stuff
        public LayerMask groundLayer;
        Vector2 HitPosition;

        private void Awake()
        {
            moveSpeed = 5.0f;
        }

        IEnumerator WaitTwo()
        {
            yield return new WaitForSeconds(0.5f);
            RayCheck();
        }

        void RayCheck()
        {
            Vector2 currentPosition = transform.position;

            //left ray
            RaycastHit2D hitLeft = Physics2D.Raycast(currentPosition, Vector2.left, RaycastDistance, groundLayer);
            //Debug.DrawRay(currentPosition, new Vector2(-5, 0), Color.green, 5);
            if (hitLeft.collider != null)
            {
                Debug.Log("left hit: " + hitLeft.point);
                PointLeft = hitLeft.point;
                if (Vector2.Distance(PointLeft, currentPosition) < distanceCheck)
                {
                    //dont spawn
                }
                else
                {
                    Debug.Log("left spawned");
                    //spawn trigger left
                    GameObject TriggerL = Instantiate(ResizableTrigger, PointLeft, Quaternion.identity);
                    TriggerL.transform.parent = gameObject.transform;
                    TriggerL.GetComponent<DetectTrigger>().ObjectToMove = this.gameObject;
                    setTriggerScale(TriggerL, currentPosition, hitLeft.transform.position);
                }
            }
            else
            {
                Debug.Log("left is clear");

                //spawn trigger up
                Vector2 SpawnPosition = new Vector2(gameObject.transform.position.x - 5.0f, gameObject.transform.position.y);
                GameObject TriggerU = Instantiate(ResizableTrigger, SpawnPosition, Quaternion.identity);
                TriggerU.transform.parent = gameObject.transform; //set parent to spike object
                TriggerU.GetComponent<DetectTrigger>().ObjectToMove = this.gameObject;
                setTriggerScale(TriggerU, currentPosition, SpawnPosition);
            }


            //right ray
            RaycastHit2D hitRight = Physics2D.Raycast(currentPosition, Vector2.right, RaycastDistance, groundLayer);

            if (hitRight.collider != null)
            {
                PointRight = hitRight.point;

                if (Vector2.Distance(PointRight, currentPosition) < distanceCheck)
                {
                    //dont spawn
                }
                else
                {
                    Debug.Log("right spawned");
                    //spawn trigger right
                    GameObject TriggerR = Instantiate(ResizableTrigger, PointRight, Quaternion.identity);
                    TriggerR.transform.parent = gameObject.transform;
                    TriggerR.GetComponent<DetectTrigger>().ObjectToMove = this.gameObject;
                    setTriggerScale(TriggerR, currentPosition, hitRight.transform.position);
                }
            }
            else
            {
                Debug.Log("right is clear");

                //spawn trigger up
                Vector2 SpawnPosition = new Vector2(gameObject.transform.position.x + 5.0f, gameObject.transform.position.y);
                GameObject TriggerU = Instantiate(ResizableTrigger, SpawnPosition, Quaternion.identity);
                TriggerU.transform.parent = gameObject.transform; //set parent to spike object
                                                                  //set movement position
                TriggerU.GetComponent<DetectTrigger>().ObjectToMove = this.gameObject;
                setTriggerScale(TriggerU, currentPosition, SpawnPosition);
            }


            //up ray
            RaycastHit2D hitUp = Physics2D.Raycast(currentPosition, Vector2.up, RaycastDistance, groundLayer);

            if (hitUp.collider != null)
            {
                if (Vector2.Distance(PointUp, currentPosition) < distanceCheck)
                {
                    //dont spawn
                }
                else
                {
                    PointUp = hitUp.point;

                    Debug.Log("up spawned");
                    //spawn trigger up
                    GameObject TriggerU = Instantiate(ResizableTrigger, PointUp, Quaternion.identity);
                    TriggerU.transform.parent = gameObject.transform; //set parent to spike object
                                                                      //set movement position
                    TriggerU.GetComponent<DetectTrigger>().ObjectToMove = this.gameObject;
                    setTriggerScale(TriggerU, currentPosition, hitUp.transform.position);
                }
            }
            else
            {
                Debug.Log("up is clear");

                //spawn trigger up
                Vector2 SpawnPosition = new Vector2(gameObject.transform.position.x, transform.position.y + 5.0f);
                GameObject TriggerU = Instantiate(ResizableTrigger, SpawnPosition, Quaternion.identity);
                TriggerU.transform.parent = gameObject.transform; //set parent to spike object
                                                                  //set movement position
                TriggerU.GetComponent<DetectTrigger>().ObjectToMove = this.gameObject;
                setTriggerScale(TriggerU, currentPosition, SpawnPosition);
            }


            //down ray
            RaycastHit2D hitDown = Physics2D.Raycast(currentPosition, Vector2.down, RaycastDistance, groundLayer);

            if (hitDown.collider != null)
            {
                PointDown = hitDown.point;

                if (Vector2.Distance(PointDown, currentPosition) < distanceCheck)
                {
                    //dont spawn
                }
                else
                {
                    Debug.Log("down spawned");
                    //spawn trigger down
                    GameObject TriggerD = Instantiate(ResizableTrigger, PointDown, Quaternion.identity);
                    TriggerD.transform.parent = gameObject.transform;
                    TriggerD.GetComponent<DetectTrigger>().ObjectToMove = this.gameObject;
                    setTriggerScale(TriggerD, currentPosition, hitDown.transform.position);
                }
            }
            else
            {
                Debug.Log("down is clear");

                //spawn trigger up
                Vector2 SpawnPosition = new Vector2(gameObject.transform.position.x, transform.position.y - 5.0f);
                GameObject TriggerU = Instantiate(ResizableTrigger, SpawnPosition, Quaternion.identity);
                TriggerU.transform.parent = gameObject.transform; //set parent to spike object
                                                                  //set movement position
                TriggerU.GetComponent<DetectTrigger>().ObjectToMove = this.gameObject;
                setTriggerScale(TriggerU, currentPosition, SpawnPosition);
            }

        }

        void setTriggerScale(GameObject TriggerObject, Vector2 startPos, Vector2 endPos)
        {
            //TriggerObject.transform.localScale = new Vector3(1, 1, 1);

            Vector2 centerPos = new Vector2((startPos.x + endPos.x) / 2, (startPos.y + endPos.y) / 2);

            float scaleX = Mathf.Abs(startPos.x - endPos.x);
            float scaleY = Mathf.Abs(startPos.y - endPos.y);

            if (scaleX < 1.0f)
                scaleX = 1.0f;

            if (scaleY < 1.0f)
                scaleY = 1.0f;

            TriggerObject.transform.position = centerPos;
            TriggerObject.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }

        private void Start()
        {
            StartCoroutine(WaitTwo());
        }

        //move object to player
        private void Update()
        {
            if (MoveTowards == true)
            {
                //move
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, tempPosition, step);

                //move towards false once there
                if (gameObject.transform.position == tempPosition)
                {
                    MoveTowards = false;
                }
            }
        }

        //kill player
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //on trigger hit kill player
            if (collision.CompareTag("Player"))
            {
                //reduce player health
                collision.GetComponent<Character>().TakeDamage(1);
            }
        }
    }


}