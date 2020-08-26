using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;
    public class InputHandler : MonoBehaviour
    {
        public bool isLogging = false;

        [SerializeField] private Joystick joystick = null;

        public Vector2 GetInputDirection()
        {
            Vector3 direction = this.joystick.Direction;
            Log(this.isLogging, Type.Message, $"Input Direction: {direction}");
            return direction;
        }
    }
}