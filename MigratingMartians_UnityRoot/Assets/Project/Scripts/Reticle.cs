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
        [SerializeField] private float lockedDistance = 0.1f;
        [Tooltip("Duration of locking time before target becomes locked.")]
        [SerializeField] private float lockedThreshold = 1.0f;
        [SerializeField] private State state = State.Searching;

        [SerializeField] private MovePhysics2D move = null;

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

        private void FixedUpdate()
        {
            if (state == State.Locked)
            {
                if(Vector2.Distance(this.transform.position, this.target.transform.position) > this.lockedDistance)
                {
                    this.FollowLockedTarget();
                }
                else
                {
                    this.move.ResetVelocity();
                }                     
            }
           
        }

        private void LateUpdate()
        {
            switch (state)
            {
                case State.Searching:
                this.SearchForTargets();
                break;
            }
        }

        private void FollowLockedTarget()
        {
            Vector2 direction = this.target.transform.position - this.transform.position;
            this.move.GlobalMove(direction.normalized);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, this.reticleRadius);
        }

        public void PlayerMove(Vector2 inputDirection)
        {
            if (inputDirection == Vector2.zero || this.state == State.Locked)
                return;

            switch (this.state)
            {
                case State.Searching:
                this.move.GlobalMove(inputDirection);
                break;

                case State.Locking:
                this.move.GlobalMove(inputDirection, this.lockingMoveSpeedMultplier);
                break;
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
                this.move.ResetVelocity();
                this.OnLocked.Invoke();
                break;
            }
        }

        private IEnumerator Locking()
        {
            yield return new WaitForSeconds(0.1f);
            this.lockingTime += 0.1f;

            // Lock acquired
            if (this.lockingTime >= this.lockedThreshold)
            {
                this.SetState(State.Locked);
                yield break;
            }

            this.reticleSprite.enabled = !this.reticleSprite.isVisible;

            Target target;
            if (this.IsOverlapingTarget(out target))
            {
                StartCoroutine(this.Locking());
                yield break;
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