using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;
    public class Weapon : MonoBehaviour
    {
        public bool isLogging = false;
        
        [SerializeField] private GameObjectPool projectilePool = null;
        [SerializeField] private Reticle reticle = null;
        [SerializeField] private Rotate2D barrelRotation = null;
        public void Aim(Vector2 direction)
        {
            this.reticle.PlayerMove(direction);
            this.barrelRotation.LookAt(this.reticle.gameObject.transform.position);
        }

        public void Fire()
        {
            Log(this.isLogging, Type.Message, $"Projectile Fired from Player");
            Projectile projectile = this.projectilePool.GetObject<Projectile>(true).GetComponent<Projectile>();
        }
    }
}