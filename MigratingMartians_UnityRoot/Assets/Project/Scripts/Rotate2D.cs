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

        [SerializeField] private Vector2 clampRotation = new Vector2(0, 180);

        private void LateUpdate()
        {
            this.ClampRotation();
        }

        private void ClampRotation()
        {
            Vector3 euler = transform.eulerAngles;
            if (euler.z > 180) euler.z = euler.z - 360;
            euler.z = Mathf.Clamp(euler.z, this.clampRotation.x, this.clampRotation.y);
            transform.eulerAngles = euler;
        }

        public void LookAt(Vector2 target)
        {
            Vector3 dir = (Vector3)target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Log(this.isLogging, Type.Message, $"Target angle {angle}");

            if (angle < 0)
            {
                if (target.x <= this.transform.position.x)
                {
                    angle = this.clampRotation.y;
                }
                else
                {
                    angle = this.clampRotation.x;
                }
            }

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
        }

        public void RotateToAngle(float angle)
        {
            Log(this.isLogging, Type.Message, $"angle: {angle}");
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
        }
    }
}