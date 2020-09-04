using UnityEngine;

namespace KyleConibear
{
    public static class ExtensionMethods
    {
        public static void DrawBounds(this Vector4 vector4, Color color, float life = 5.0f)
        {
            Debug.DrawLine(new Vector3(vector4.x, vector4.z, 0), new Vector3(vector4.y, vector4.z, 0), color, life);
            Debug.DrawLine(new Vector3(vector4.y, vector4.z, 0), new Vector3(vector4.y, vector4.w, 0), color, life);
            Debug.DrawLine(new Vector3(vector4.y, vector4.w, 0), new Vector3(vector4.x, vector4.w, 0), color, life);
            Debug.DrawLine(new Vector3(vector4.x, vector4.w, 0), new Vector3(vector4.x, vector4.z, 0), color, life);
        }
    }
}