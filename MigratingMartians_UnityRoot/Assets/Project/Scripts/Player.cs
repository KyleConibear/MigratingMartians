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
        [SerializeField] private MovePhysics2D drive = null;

        [SerializeField] private Reticle reticle = null;
        
        [SerializeField] private Rotate2D barrelRotation = null;

        private void Update()
        {
            this.reticle.Move(this.inputHandler.GetAimInputDirection());
            this.barrelRotation.LookAt(this.reticle.gameObject.transform.position);
        }
        private void FixedUpdate()
        {
            this.drive.GlobalMove(this.inputHandler.GetDriveInputDirection());            
        }      
    }
}