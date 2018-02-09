using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP
{
    public class CharacterController : MonoBehaviour
    {
        #region referenced components
        Character character;
        #endregion

        #region Unity Runtime methods
        void Start()
        {
            character = GetComponent<Character>();
        }
        
        void Update()
        {
            // Directional movement
            character.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime);

            // Jump button pressed
            if (Input.GetButtonDown("Jump"))
            {
                character.Jump();
            }

            // Attack Button pressed
            if (Input.GetButtonDown("Attack"))
            {
                character.Attack();
            }
        }
        #endregion
    }
}
