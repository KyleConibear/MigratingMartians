using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;
    public class Weapons : MonoBehaviour
    {
        public bool isLogging = false;
        
        [SerializeField] private GameObjectPool projectilePool = null;
        public void Fire()
        {
            Log(this.isLogging, Type.Message, $"Projectile Fired from Player");
            Projectile projectile = this.projectilePool.GetObject<Projectile>(true).GetComponent<Projectile>();
        }
    }
}