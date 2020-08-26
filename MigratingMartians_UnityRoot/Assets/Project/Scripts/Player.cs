using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(Movement2D))]
    public class Player : MonoBehaviour
    {
        private InputHandler inputHandler = null;
        private Movement2D movement2D = null;

        private void Awake()
        {
            this.inputHandler = this.GetComponent<InputHandler>();
            this.movement2D = this.GetComponent<Movement2D>();
        }

        private void FixedUpdate()
        {
            this.movement2D.Move(this.inputHandler.GetInputDirection());
        }
    }
}