using UnityEngine;

namespace KyleConibear
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get a random position within Vector4 Bounds.
        /// </summary>
        /// <param name="vector4">The bounds. x:left y:right z:top w:bottom</param>
        /// <returns>Random position within bounds</returns>
        public static Vector2 GetRandomPositionWithBounds(this Vector4 vector4)
        {
            return new Vector2(Random.Range(vector4.x, vector4.y), Random.Range(vector4.z, vector4.w));
        }

        public static void DrawBounds(this Vector4 vector4, Color color, float life = 5.0f)
        {
            Debug.DrawLine(new Vector3(vector4.x, vector4.z, 0), new Vector3(vector4.y, vector4.z, 0), color, life);
            Debug.DrawLine(new Vector3(vector4.y, vector4.z, 0), new Vector3(vector4.y, vector4.w, 0), color, life);
            Debug.DrawLine(new Vector3(vector4.y, vector4.w, 0), new Vector3(vector4.x, vector4.w, 0), color, life);
            Debug.DrawLine(new Vector3(vector4.x, vector4.w, 0), new Vector3(vector4.x, vector4.z, 0), color, life);
        }

        public static bool IsWithinBounds(this Vector2 vector2, Vector4 bounds)
        {            
            // Check left
            if(vector2.x < bounds.x)
            {
                return false;
            }
            // Check right
            else if(vector2.x > bounds.y)
            {
                return false;
            }
            // Check top
            else if(vector2.y > bounds.z)
            {
                return false;
            }
            // Check bottom
            else if(vector2.y < bounds.w)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        public static bool OutOfRange(this Transform transform, Vector3 target, float range)
        {
            float distance = Vector3.Distance(transform.position, target);
            return distance > range;
        }
    }
}