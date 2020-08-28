using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;   
    public class Player : MonoBehaviour
    {
        public bool isLogging = false;

        [SerializeField] private InputHandler inputHandler = null;
        [SerializeField] private Move2D driveMovement = null;

        [SerializeField] private Reticle reticle = null;
        
        [SerializeField] private Rotate2D barrelRotation = null;

        private void Update()
        {
            this.reticle.Move(this.inputHandler.GetAimInputDirection());
            this.barrelRotation.LookAt(this.reticle.gameObject.transform.position);
        }

        private void FixedUpdate()
        {
            this.driveMovement.Move(Move2D.Type.Physics, this.inputHandler.GetDriveInputDirection());            
        }
    }
}