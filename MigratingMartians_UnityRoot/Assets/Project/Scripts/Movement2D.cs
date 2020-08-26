using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement2D : MonoBehaviour
    {
        public bool isLogging = false;

        [Range(10, 100)]
        [SerializeField] private float speed = 50.0f;

        [SerializeField] private new PolygonCollider2D collider = null;

        private Vector3 lastAcceptedPosition = Vector3.negativeInfinity;

        private new Rigidbody2D rigidbody2D = null;

        private void Awake()
        {
            this.rigidbody2D = this.GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            if (this.IsWithinBounds() == false)
            {
                this.transform.position = this.lastAcceptedPosition;
            }

            Log(this.isLogging, Type.Message, $"Movement Direction: {direction}");
            this.rigidbody2D.velocity = direction * speed * Time.deltaTime;
            this.lastAcceptedPosition = this.transform.position;
        }

        private bool IsWithinBounds()
        {
            return this.collider.bounds.Contains(this.transform.position);
        }
    }
}