using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;
    [RequireComponent(typeof(MovePhysics2D))]
    public class Projectile : MonoBehaviour
    {
        private MovePhysics2D physics = null;

        

        private void Awake()
        {
            this.physics = this.GetComponent<MovePhysics2D>();
        }

        private void Start()
        {
            this.physics.ResetVelocity();
        }

        private void LateUpdate()
        {
            if(GameManager.IsPositionOnScreen(this.transform.position) == false)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            this.physics.AddRelativeForce(Vector2.right);
        }
    }
}