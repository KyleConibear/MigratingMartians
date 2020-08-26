using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;
    [RequireComponent(typeof(InputHandler))]    
    public class Player : MonoBehaviour
    {
        public bool isLogging = false;

        private InputHandler inputHandler = null;
        [SerializeField] private Movement2D movement2D = null;

        private void Awake()
        {
            this.inputHandler = this.GetComponent<InputHandler>();

            if(this.movement2D == null)
            {
                Log(this.isLogging, Type.Warning, $"{this.name} does not have Movement2D assigned in the inspector.");
                this.movement2D = this.GetComponentInChildren<Movement2D>();
            }            
        }

        private void FixedUpdate()
        {
            this.movement2D.Move(this.inputHandler.GetInputDirection());
        }
    }
}