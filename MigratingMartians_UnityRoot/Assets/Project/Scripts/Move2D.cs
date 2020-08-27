using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    public class Move2D : MonoBehaviour
    {
        public bool isLogging = false;

        [SerializeField] private Type type = Type.Physics;
        public enum Type
        {
            Physics,
            Transform
        }

        [Range(1, 100)]
        [SerializeField] private float speed = 50.0f;

        [Header("Optional. Only required for type Physics")]
        [SerializeField] private new Rigidbody2D rigidbody2D = null;

        public void Move(Vector2 direction)
        {
            Log(this.isLogging, Logger.Type.Message, $"Movement Direction: {direction}");

            switch (this.type)
            {
                case Type.Transform:
                Vector3 targetPosition = this.transform.position + ((Vector3)(direction * speed * Time.deltaTime));
                this.transform.position = targetPosition;
                break;

                default:
                this.rigidbody2D.velocity = direction * speed * Time.deltaTime;
                break;
            }
        }
    }
}