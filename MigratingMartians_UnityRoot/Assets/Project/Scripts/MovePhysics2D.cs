using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePhysics2D : MonoBehaviour
{
    [Range(1,100)]
    [SerializeField] private float velocity = 50.0f;

    private new Rigidbody2D rigidbody2D = null;

    private void Awake()
    {
        this.rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    public void GlobalMove(Vector2 direction, float velocityMultiplier = 1.0f)
    {
        this.rigidbody2D.velocity = direction * (velocity * velocityMultiplier) * Time.deltaTime;
    }

    public void AddForce(Vector2 direction, float velocityMultiplier = 1.0f)
    {
        direction *= this.velocity * velocityMultiplier;

        this.rigidbody2D.AddForce(direction);
    }

    public void AddRelativeForce(Vector2 direction, float velocityMultiplier = 1.0f)
    {
        direction *= this.velocity * velocityMultiplier;

        this.rigidbody2D.AddRelativeForce(direction);
    }

    public void ResetVelocity()
    {
        this.rigidbody2D.velocity = Vector2.zero;
    }
}
