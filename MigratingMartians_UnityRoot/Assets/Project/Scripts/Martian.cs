using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    [RequireComponent(typeof(Meter))]
    public class Martian : MonoBehaviour
    {        
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

        private void OnCollisionEnter(Collision collision)
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                this.Hit(projectile.damage);
            }
        }

        public void Hit(uint damage)
        {
            this.healthMeter.Subtract(damage);
        }

        private void Killed()
        {
            On_MartianKilled.Invoke(this);
            this.gameObject.SetActive(false);
        }
    }
}