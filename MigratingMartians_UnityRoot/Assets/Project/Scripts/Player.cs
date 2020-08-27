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
        [SerializeField] private Move2D driveMovement = null;
        [SerializeField] private Move2D reticleMove = null;
        [SerializeField] private LookAt2D barrelRotation = null;

        private void Awake()
        {
            this.inputHandler = this.GetComponent<InputHandler>();

            if(this.driveMovement == null)
            {
                Log(this.isLogging, Type.Warning, $"{this.name} does not have Movement2D assigned in the inspector.");
                this.driveMovement = this.GetComponentInChildren<Move2D>();
            }            
        }

        private void FixedUpdate()
        {
            this.driveMovement.Move(this.inputHandler.GetDriveInputDirection());
            this.reticleMove.Move(this.inputHandler.GetAimInputDirection());
            this.barrelRotation.RotateTowards(this.reticleMove.gameObject.transform.position);
        }
    }
}