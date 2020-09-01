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

        [SerializeField] private Weapon weapon = null;
        
        private void FixedUpdate()
        {
            this.drive.GlobalMove(this.inputHandler.GetDriveInputDirection());
            this.weapon.Aim(this.inputHandler.GetAimInputDirection());
        }      
    }
}