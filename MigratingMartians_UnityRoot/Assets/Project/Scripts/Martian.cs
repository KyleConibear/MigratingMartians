using System;
using UnityEngine;
using TMPro;

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
        [SerializeField] private TMP_Text debugText = null;

        [Header("Components")]
        [SerializeField] private Animator animator = null;

        public static Action<Martian> On_MartianKilled;

        [SerializeField] private int rewardPoints = 100;
        public int RewardPoints => this.rewardPoints;

        private Meter healthMeter = null;

        private ClampTransform clampTransform = null;

        [SerializeField] private Vector2 nextPosition = Vector2.zero;
        [Range(1, 3)]
        [SerializeField] private float stopDistance;

        private MovePhysics2D movePhysics = null;

        [SerializeField] private float bodyAngle = 15.0f;
        [SerializeField] private float leftJetAngle = 30.0f;
        [SerializeField] private float rightJetAngle = 30.0f;
        [SerializeField] private Rotate2D bodyRotation = null;
        [SerializeField] private Rotate2D leftJetRotation = null;
        [SerializeField] private Rotate2D rightJetRotation = null;
        [SerializeField] private ParticleSystem leftJetPS = null;
        [SerializeField] private ParticleSystem rightJetPS = null;
        [Range(6,32)]
        [SerializeField] private float psSpeedChange = 12;
        [Range(1,12)]
        [SerializeField] private Vector2 psSpeedMultiplierRange = new Vector2(1, 6);
        private float defaultSpeed;
        private void Awake()
        {
            this.clampTransform = this.GetComponent<ClampTransform>();
            this.movePhysics = this.GetComponent<MovePhysics2D>();
            this.healthMeter = this.GetComponent<Meter>();

            this.defaultSpeed = this.psSpeedMultiplierRange.x + (this.psSpeedMultiplierRange.y - this.psSpeedMultiplierRange.x);
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

        private void LateUpdate()
        {
            this.Rotate();
            this.SetParticleSpeed();
        }

        private void FixedUpdate()
        {
            // If we our are out of range of our nextPosition
            if (Vector2.Distance((Vector2)this.transform.position, this.nextPosition) >= this.stopDistance)
            {
                // Move towards nextPosition
                this.movePhysics.AddForceTowardsTarget(this.nextPosition);
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
            // Log(this.isLogging, this.name, Type.Message, $"nextPosition: {this.nextPosition}");
        }

        private void Rotate()
        {
            // Moving left
            if (this.movePhysics.GetVelocity().x > 0)
            {
                this.leftJetRotation.RotateToAngle(-this.leftJetAngle);
                this.rightJetRotation.RotateToAngle(-this.rightJetAngle);
                this.bodyRotation.RotateToAngle(-this.bodyAngle);
            }
            // Moving right
            else if (this.movePhysics.GetVelocity().x < 0)
            {
                this.leftJetRotation.RotateToAngle(this.leftJetAngle);
                this.rightJetRotation.RotateToAngle(this.rightJetAngle);
                this.bodyRotation.RotateToAngle(this.bodyAngle);
            }
        }

        private void DebugText(string message)
        {
            this.debugText.text = message;
        }

        private void SetParticleSpeed()
        {
            // Moving Down
            if (this.movePhysics.GetVelocity().y < Mathf.Epsilon)
            {
                // Moving left
                if (this.movePhysics.GetVelocity().x < Mathf.Epsilon)
                {
                    this.DebugText("DL");
                    this.IncreaseRightJetParticleSpeed();
                    this.DecreaseLeftJetParticleSpeed();                    
                }
                // Moving right
                else if (this.movePhysics.GetVelocity().x > Mathf.Epsilon)
                {
                    this.DebugText("DR");
                    this.IncreaseLeftJetParticleSpeed();
                    this.DecreaseRightJetParticleSpeed();
                }
            }
            // Moving Up
            else if (this.movePhysics.GetVelocity().y > Mathf.Epsilon)
            {
                // Moving left
                if (this.movePhysics.GetVelocity().x < Mathf.Epsilon)
                {
                    this.DebugText("UL");
                    this.IncreaseLeftJetParticleSpeed();
                    this.IncreaseRightJetParticleSpeed();
                }
                // Moving right
                else if (this.movePhysics.GetVelocity().x > Mathf.Epsilon)
                {
                    this.DebugText("UR");
                    this.IncreaseLeftJetParticleSpeed();
                    this.IncreaseRightJetParticleSpeed();
                }
            }
        }


        private void IncreaseLeftJetParticleSpeed()
        {            
            var leftJet = this.leftJetPS.main;
            if (leftJet.startSpeedMultiplier >= this.psSpeedMultiplierRange.y)
                return;
            leftJet.startSpeedMultiplier = this.leftJetPS.main.startSpeedMultiplier + (this.psSpeedChange * Time.deltaTime);
        }

        private void DecreaseLeftJetParticleSpeed()
        {
            var leftJet = this.leftJetPS.main;
            if (leftJet.startSpeedMultiplier <= this.psSpeedMultiplierRange.x)
                return;
            leftJet.startSpeedMultiplier = this.leftJetPS.main.startSpeedMultiplier - (this.psSpeedChange * Time.deltaTime);
        }

        private void IncreaseRightJetParticleSpeed()
        {
            var rightJet = this.rightJetPS.main;
            if (rightJet.startSpeedMultiplier >= this.psSpeedMultiplierRange.y)
                return;
            rightJet.startSpeedMultiplier = this.rightJetPS.main.startSpeedMultiplier + (this.psSpeedChange * Time.deltaTime);
        }

        private void DecreaseRightJetParticleSpeed()
        {
            var rightJet = this.rightJetPS.main;
            if (rightJet.startSpeedMultiplier <= this.psSpeedMultiplierRange.x)
                return;
            rightJet.startSpeedMultiplier = this.rightJetPS.main.startSpeedMultiplier - (this.psSpeedChange * Time.deltaTime);
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