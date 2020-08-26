using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement2D : MonoBehaviour
    {
        public bool isLogging = false;
        
        [Range(10,100)]
        [SerializeField] private float speed = 10.0f;

        private new Rigidbody2D rigidbody2D = null;

        private void Awake()
        {
            this.rigidbody2D = this.GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            Log(this.isLogging, Type.Message, $"Movement Direction: {direction}");
            this.rigidbody2D.velocity = direction * speed * Time.deltaTime;
        }
    }
}