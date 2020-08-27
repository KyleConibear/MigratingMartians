using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;
    public class InputHandler : MonoBehaviour
    {
        public bool isLogging = false;

        [SerializeField] private Joystick driveJoystick = null;
        [SerializeField] private Joystick aimJoyStick = null;
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
    }
}