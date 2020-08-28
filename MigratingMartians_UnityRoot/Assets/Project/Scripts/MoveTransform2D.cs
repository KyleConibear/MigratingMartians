using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    public class MoveTransform2D : MonoBehaviour
    {
        public bool isLogging = false;

        [Range(1, 100)]
        [SerializeField] private float speed = 50.0f;

        public void Move(Vector2 direction, float speedMultiplier = 1.0f)
        {
            Log(this.isLogging, Logger.Type.Message, $"Movement Direction: {direction}");

            Vector3 targetPosition = this.transform.position + ((Vector3)(direction * (speed * speedMultiplier) * Time.deltaTime));
            this.transform.position = targetPosition;
        }
    }
}