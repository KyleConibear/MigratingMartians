using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    [RequireComponent(typeof(ClampTransform))]
    [RequireComponent(typeof(MovePhysics2D))]
    [RequireComponent(typeof(Meter))]
    public class Martian : MonoBehaviour
    {
        [Header("Debugging")]
        public bool isLogging = false;

        [Header("Components")]
        [SerializeField] private Animator animator = null;

        public static Action<Martian> On_MartianKilled;

        [SerializeField] private int rewardPoints = 0;
        public int RewardPoints => this.rewardPoints;

        private Meter healthMeter = null;

         private ClampTransform clampTransform = null;

        [SerializeField] private Vector2 nextPosition = Vector2.zero;
        [Range(1,3)]
        [SerializeField] private float stopDistance;

        private MovePhysics2D physics = null;

        private void Awake()
        {
            this.clampTransform = this.GetComponent<ClampTransform>();
            this.physics = this.GetComponent<MovePhysics2D>();
            this.healthMeter = this.GetComponent<Meter>();
        }

        private void Start()
        {
            this.healthMeter.OnDepleted.AddListener(Killed);
            this.SetNextPosition();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                this.Hit(projectile.damage, collision.transform.position.x);
            }
        }

        private void Update()
        {
            // If we our are out of range of our nextPosition
            if (Vector2.Distance((Vector2)this.transform.position, this.nextPosition) >= this.stopDistance)
            {
                // Move towards nextPosition
                this.physics.MoveToTarget(this.nextPosition);
            }
            else
            {
                // Get/Set the nextPosition
                this.SetNextPosition();
            }
        }

        //private void LateUpdate()
        //{
        //    // Check if our clamp is disabled
        //    if(this.clampTransform.enabled == false)
        //    {
        //        // Check if our position is within the clamps bounds
        //        Vector2 ourPosition = (Vector2)this.transform.position;
        //        if (ourPosition.IsWithinBounds(this.clampTransform.bounds))
        //        {
        //            // Enable clamp
        //            this.clampTransform.enabled = true;
        //        }
        //    }
        //}

        private void SetNextPosition()
        {            
            this.nextPosition = this.clampTransform.bounds.GetRandomPositionWithBounds();            
            Log(this.isLogging, this.name, Type.Message, $"nextPosition: {this.nextPosition}");
        }

        private void Hit(uint damage, float collisionX)
        {
            Log(this.isLogging, Type.Message, $"{damage} damage taken.");

            // Reduce health
            this.healthMeter.Subtract(damage);

            // Flip the hurt animation based on what side was hit
            if (collisionX < this.transform.position.x)
            {
                // Animate hit left
                this.animator.SetTrigger("onHitLeft");
            }
            else
            {
                // Animate hit right
                this.animator.SetTrigger("onHitRight");
            }
        }
        private void Killed()
        {
            On_MartianKilled.Invoke(this);
            this.gameObject.SetActive(false);
        }
    }
}