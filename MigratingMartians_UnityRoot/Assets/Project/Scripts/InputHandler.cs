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
            return this.joystick.Direction;
        }
    }
}