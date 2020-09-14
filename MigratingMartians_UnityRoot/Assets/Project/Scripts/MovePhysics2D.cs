using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    [RequireComponent(typeof(Rigidbody2D))]
    public class MovePhysics2D : MonoBehaviour
    {
        public bool isLogging = false;

        [Range(1, 1000)]
        [SerializeField] private float maxVelocity = 50.0f;

        [Range(1, 500)]
        [SerializeField] private float velocity = 50.0f;

        [Range(1, 500)]
        [SerializeField] private float force = 100.0f;

        [SerializeField] private new Rigidbody2D rigidbody2D = null;        

        private void Awake()
        {
            this.rigidbody2D = this.GetComponent<Rigidbody2D>();
        }

        public Vector2 GetVelocity()
        {
            return this.rigidbody2D.velocity;
        }

        public void GlobalMove(Vector2 direction, float velocityMultiplier = 1.0f)
        {
            this.rigidbody2D.velocity = direction * (velocity * velocityMultiplier) * Time.fixedDeltaTime;
        }

        public void AddForce(Vector2 direction, float velocityMultiplier = 1.0f)
        {
            direction *= this.velocity * velocityMultiplier;

            this.rigidbody2D.AddForce(direction);
        }

        public void MoveToTarget(Vector2 target)
        {
            Vector2 direction = target - (Vector2)this.transform.position;
            //direction = direction.normalized;
            Log(this.isLogging, this.name, Type.Message, $"direction: {direction}");
            this.GlobalMove(direction);
        }

        /// <summary>
        /// Move an object along x and z axis by applying force in the given direction
        /// </summary>
        /// <param name="direction">"direction.y" will be converted to the z axis</param>
        public void AddForce(Vector3 direction, ForceMode2D mode = ForceMode2D.Force)
        {
            Vector3 force = direction * this.force;
            this.rigidbody2D.AddForce(force, mode);
            this.RestrictVelocity();
        }

        public void AddForceTowardsTarget(Vector3 target, ForceMode2D mode = ForceMode2D.Force)
        {
            Vector3 direction = target - this.transform.position;
            this.AddForce(direction.normalized, mode);
        }

        public void AddRelativeForce(Vector2 direction, float velocityMultiplier = 1.0f)
        {
            direction *= this.velocity * velocityMultiplier;

            this.rigidbody2D.AddRelativeForce(direction);
        } 

        public void ResetVelocity()
        {
            this.rigidbody2D.velocity = Vector2.zero;
        }

        private void RestrictVelocity()
        {
            float xClamp = Mathf.Clamp(this.rigidbody2D.velocity.x, -this.maxVelocity, this.maxVelocity);
            float yClamp = Mathf.Clamp(this.rigidbody2D.velocity.y, -this.maxVelocity, this.maxVelocity);
            Vector2 restrictedVelocity = new Vector3(xClamp, yClamp);
            this.rigidbody2D.velocity = restrictedVelocity;
        }

        public void SetIsKinematic(bool enabled)
        {
            this.rigidbody2D.isKinematic = enabled;
        }
    }
}