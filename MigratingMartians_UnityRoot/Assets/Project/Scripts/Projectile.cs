using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KyleConibear
{
    using static Logger;
    [RequireComponent(typeof(MovePhysics2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private uint _damage = 1;
        public uint damage => this._damage;
        [SerializeField] private GameObject hitParticle = null;
        [SerializeField] private float hitVFXLife = 1.0f;

        private MovePhysics2D physics = null;

        private bool hasCollided = false;

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
            if (GameManager.IsPositionOnScreen(this.transform.position) == false)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            this.physics.AddRelativeForce(Vector2.right);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (this.hasCollided == false && collision.gameObject.GetComponent<Projectile>() == false)
            {
                this.hasCollided = true;               

                this.physics.SetIsKinematic(true);
                this.physics.ResetVelocity();

                this.hitParticle.SetActive(true);
                StartCoroutine(this.DisableGameobject(this.hitVFXLife));
            }
        }

        private void OnDisable()
        {
            this.physics.SetIsKinematic(false);
            this.hitParticle.gameObject.SetActive(false);
            hasCollided = false;
        }

        private IEnumerator DisableGameobject(float delay)
        {
            yield return new WaitForSeconds(delay);

            this.gameObject.SetActive(false);
        }
    }
}