using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    public class Move2D : MonoBehaviour
    {
        public bool isLogging = false;

        public enum Type
        {
            Physics,
            Transform
        }

        [Range(1, 100)]
        [SerializeField] private float speed = 50.0f;

        [Header("Optional. Only required for type Physics")]
        [SerializeField] private new Rigidbody2D rigidbody2D = null;

        public void Move(Type type, Vector2 direction, float speedMultiplier = 1.0f)
        {
            Log(this.isLogging, Logger.Type.Message, $"Movement Direction: {direction}");

            switch (type)
            {
                case Type.Transform:
                Vector3 targetPosition = this.transform.position + ((Vector3)(direction * (speed * speedMultiplier) * Time.deltaTime));
                this.transform.position = targetPosition;
                break;

                default:
                this.rigidbody2D.velocity = direction * (speed * speedMultiplier) * Time.deltaTime;
                break;
            }
        }
    }
}