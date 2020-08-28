using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KyleConibear
{
    using static Logger;
    public class InputHandler : MonoBehaviour
    {
        public bool isLogging = false;

        [SerializeField] private Reticle reticle = null;

        [SerializeField] private Joystick driveJoystick = null;
        [SerializeField] private Joystick aimJoyStick = null;
        [SerializeField] private GameObject lockedButtonContainer = null;

        private void Awake()
        {
            this.reticle.OnLocked.AddListener(this.EnableLockedButtons);
            this.reticle.OnUnlocked.AddListener(this.EnableAimJoystick);
        }

        public Vector2 GetDriveInputDirection()
        {
            Vector3 direction = this.driveJoystick.Direction;
            Log(this.isLogging, Type.Message, $"Input Drive Direction: {direction}");
            return direction;
        }

        public Vector2 GetAimInputDirection()
        {
            Vector3 direction = this.aimJoyStick.Direction;
            Log(this.isLogging, Type.Message, $"Input Aim Direction: {direction}");
            return direction;
        }

        private void EnableAimJoystick()
        {
            this.lockedButtonContainer.SetActive(false);
            this.aimJoyStick.gameObject.SetActive(true);
        }

        private void EnableLockedButtons()
        {
            this.lockedButtonContainer.SetActive(true);
            this.aimJoyStick.gameObject.SetActive(false);
        }
    }
}