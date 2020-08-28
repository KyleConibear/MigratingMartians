using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    using static Logger;
    public class Rotate2D : MonoBehaviour
    {
        public bool isLogging = false;

        [SerializeField] private float turnSpeed = 1.0f;

        [SerializeField] private Vector2 restrictRotation = new Vector2(0, 180);

        private void LateUpdate()
        {
            // this.ClampRotation();
        }

        public void LookAt(Vector2 target)
        {
            Vector3 dir = (Vector3)target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Log(this.isLogging, Type.Message, $"Target angle {angle}");

            if (angle < 0)
            {
                if(target.x <= this.transform.position.x)
                {
                    angle = this.restrictRotation.y;
                }
                else
                {
                    angle = this.restrictRotation.x;
                }
            }

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
        }

        private void ClampRotation()
        {
            Vector3 euler = this.transform.eulerAngles;

            if (euler.z < this.restrictRotation.x)
            {
                euler.z = this.restrictRotation.x;
            }
            else if (euler.z > this.restrictRotation.y)
            {
                euler.z = this.restrictRotation.y;
            }

            this.transform.eulerAngles = euler;
        }
    }
}