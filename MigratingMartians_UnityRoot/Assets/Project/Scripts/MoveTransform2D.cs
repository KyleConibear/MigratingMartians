using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    public class MoveTransform2D : MonoBehaviour
    {
        public bool isLogging = false;

        Transform target;
        float smoothTime = 0.3f;
        float yVelocity = 0.0f;

        public void Move(Vector2 direction, float speedMultiplier = 1.0f)
        {
            Log(this.isLogging, Logger.Type.Message, $"Movement Direction: {direction}");

            float newPosition = Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);
            transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
        }
    }
}