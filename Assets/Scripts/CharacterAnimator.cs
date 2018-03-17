﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator animator;
        private Character character;
       
        void Start()
        {
            animator = GetComponent<Animator>();


            character = GetComponent<Character>();

            character.ActionHandler += OnCharacterAction;
        }
        
        void Update()
        {
            if (animator != null)
            {
                animator.SetFloat("MovementX", character.Movement.x);
                animator.SetFloat("MovementY", character.Movement.y);
            }
        }

        void FixedUpdate()
        {
            if (animator != null)
            {
                animator.SetFloat("VelocityX", character.Velocity.x);
                animator.SetFloat("VelocityY", character.Velocity.y);
            }
        }

        void OnCharacterAction(Character.Action action)
        {
            if (animator != null)
            {
                animator.SetTrigger(action.ToString().ToLower());
            }
        }
    }
}
