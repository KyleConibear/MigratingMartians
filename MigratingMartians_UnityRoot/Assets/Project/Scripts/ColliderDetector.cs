using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ColliderDetector : MonoBehaviour
{
    public UnityEvent OnCollisionEnter = new UnityEvent();
    public Collision2D collision = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.collision = collision;
        OnCollisionEnter.Invoke();
    }
}
