using UnityEngine;

namespace KyleConibear
{
    using static Logger;

    public class ClampTransform : MonoBehaviour
    {
        [Header("Debugging")]
        public bool isLogging = false;
        public Color debugLineColour = Color.cyan;
        public float debugLineLife = 3.0f;

        [Header("Default: Screen size.")]
        [Tooltip("x:left y:right z:top w:bottom")]
        [SerializeField] private Vector4 _bounds = Vector4.positiveInfinity;
        public Vector4 bounds => this._bounds;
        private void Start()
        {
            if (this._bounds.x == Mathf.Infinity)
            {
                Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

                this._bounds = new Vector4(-screenBounds.x, screenBounds.x, screenBounds.y, -screenBounds.y);
                Log(this.isLogging, Type.Message, $"bounds set to screen size. {_bounds}");
            }
            
            _bounds.DrawBounds(this.debugLineColour, this.debugLineLife);
        }

        private void LateUpdate()
        {
            this.Clamp();
        }

        private void Clamp()
        {
            Vector2 clampedPosition = this.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, this._bounds.x, this._bounds.y);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, this._bounds.w, this._bounds.z);
            this.transform.position = clampedPosition;
        }
    }
}