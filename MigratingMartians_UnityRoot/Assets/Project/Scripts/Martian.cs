using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    [RequireComponent(typeof(Meter))]
    public class Martian : MonoBehaviour
    {
        public bool isLogging = false;

        [Header("Components")]
        [SerializeField] private Animator animator = null;

        public static Action<Martian> On_MartianKilled;

        [SerializeField] private int rewardPoints = 0;
        public int RewardPoints => this.rewardPoints;

        private Meter healthMeter = null;

        private void Awake()
        {
            this.healthMeter = this.GetComponent<Meter>();
        }

        private void Start()
        {
            this.healthMeter.OnDepleted.AddListener(Killed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                this.Hit(projectile.damage, collision.transform.position.x);
            }
        }

        private void Hit(uint damage, float collisionX)
        {
            Log(this.isLogging, Type.Message, $"{damage} damage taken.");

            // Reduce health
            this.healthMeter.Subtract(damage);

            // Flip the hurt animation based on what side was hit
            if(collisionX < this.transform.position.x)
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