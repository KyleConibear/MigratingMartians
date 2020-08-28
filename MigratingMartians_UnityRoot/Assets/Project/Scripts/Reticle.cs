using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KyleConibear
{
    using static Logger;

    [RequireComponent(typeof(SpriteRenderer))]
    public class Reticle : MonoBehaviour
    {
        public bool isLogging = false;

        private SpriteRenderer reticleSprite = null;

        [SerializeField] private float reticleRadius = 1.0f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Target target = null;
        [SerializeField] private float lockingTime = 0.0f;
        [SerializeField] private float lockingMoveSpeedMultplier = 0.6f;

        [Tooltip("Duration of locking time before target becomes locked.")]
        [SerializeField] private float lockedThreshold = 1.0f;
        [SerializeField] private State state = State.Searching;

        [SerializeField] private Move2D move = null;

        public UnityEvent OnLocked = new UnityEvent();
        public UnityEvent OnUnlocked = new UnityEvent();

        public enum State
        {
            Searching,
            Locking,
            Locked
        }

        private void Awake()
        {
            this.reticleSprite = this.GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            switch (state)
            {
                case State.Searching:
                this.SearchForTargets();
                break;

                case State.Locked:
                this.transform.position = this.target.transform.position;
                break;
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, this.reticleRadius);
        }

        public void Move(Vector2 direction)
        {
            if(this.state == State.Locking)
            {
                this.move.Move(Move2D.Type.Transform, direction, this.lockingMoveSpeedMultplier);
            }
            else
            {
                this.move.Move(Move2D.Type.Transform, direction);
            }            
        }

        public void UnlockTarget()
        {
            this.OnUnlocked.Invoke();
            this.SetState(State.Searching);
        }

        private void SearchForTargets()
        {
            Target target;
            if (this.IsOverlapingTarget(out target))
            {
                this.target = target;
                this.SetState(State.Locking);
            }
        }

        private void SetState(State state)
        {
            Log(this.isLogging, Type.Message, $"State changed to \"{state}\"");

            this.reticleSprite.enabled = true;

            this.state = state;

            switch (this.state)
            {
                case State.Searching:
                this.reticleSprite.color = Color.red;
                break;

                case State.Locking:
                this.reticleSprite.color = Color.blue;
                StartCoroutine(this.Locking());
                break;

                case State.Locked:                
                this.reticleSprite.color = Color.green;
                this.lockingTime = 0.0f;
                this.OnLocked.Invoke();
                break;
            }
        }

        private IEnumerator Locking()
        {
            yield return new WaitForSeconds(0.1f);
            this.lockingTime += 0.1f;

            // Lock acquired
            if(this.lockingTime >= this.lockedThreshold)
            {
                this.SetState(State.Locked);
                yield break;
            }

            this.reticleSprite.enabled = !this.reticleSprite.isVisible;

            Target target;
            if (this.IsOverlapingTarget(out target))
            {
                StartCoroutine(this.Locking());
            }
            else
            {
                this.lockingTime = 0;
                this.SetState(State.Searching);
            }
        }

        private bool IsOverlapingTarget(out Target target)
        {
            target = null;
            // Find all of the colliders on this layerMask overlaping our Reticle
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, this.reticleRadius, this.layerMask);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                Target tempTarget;
                hitColliders[i].TryGetComponent<Target>(out tempTarget);
                if (tempTarget != null)
                {
                    target = tempTarget;
                    return true;
                }
            }
            return false;
        }
    }
}