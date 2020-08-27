using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampTransform : MonoBehaviour
{
    [Header("If a custom value is not provided the bound will default to the screen size.")]
    [Tooltip("x:-x y:+x z:-y w:y")]
    [SerializeField] private Vector4 bounds = Vector4.positiveInfinity;

    [SerializeField] private float debugLinesLife = Mathf.Infinity;
    // Start is called before the first frame update
    private void Start()
    {
        if (this.bounds.x == Mathf.Infinity)
        {
            Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            this.bounds = new Vector4(-screenBounds.x, screenBounds.x, -screenBounds.y, screenBounds.y);
        }

        this.DrawBounds();
    }

    private void LateUpdate()
    {
        this.Clamp();
    }

    private void Clamp()
    {
        Vector2 clampedPosition = this.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, this.bounds.x, this.bounds.y);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, this.bounds.z, this.bounds.w);
        this.transform.position = clampedPosition;
    }

    private void DrawBounds()
    {
        // Draw clamp in editor
        Vector2 topLeft = new Vector2(this.bounds.x, this.bounds.w);
        Vector2 topRight = new Vector2(this.bounds.y, this.bounds.w);
        Vector2 bottomLeft = new Vector2(this.bounds.x, this.bounds.z);
        Vector2 bottomRight = new Vector2(this.bounds.y, this.bounds.z);
        // Top left to Top Right
        Debug.DrawLine(topLeft, topRight, Color.red, this.debugLinesLife);
        // Top Right to Bottom Right
        Debug.DrawLine(topRight, bottomRight, Color.red, this.debugLinesLife);
        // Bottom Right to Bottom Left
        Debug.DrawLine(bottomRight, bottomLeft, Color.red, this.debugLinesLife);
        // Bottom Left to Top Left
        Debug.DrawLine(bottomLeft, topLeft, Color.red, this.debugLinesLife);
    }
}
