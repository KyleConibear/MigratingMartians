using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampTransform : MonoBehaviour
{
    [Header("If a custom value is not provided the bound will default to the screen size.")]
    [Tooltip("x:left y:right z:top w:bottom")]
    [SerializeField] private Vector4 bounds = Vector4.positiveInfinity;

    public float debugLineLife = 3.0f;
    public Color debugLineColour = Color.cyan;
    private void Start()
    {
        if (this.bounds.x == Mathf.Infinity)
        {
            Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            this.bounds = new Vector4(-screenBounds.x, screenBounds.x, screenBounds.y, -screenBounds.y);
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
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, this.bounds.w, this.bounds.z);
        this.transform.position = clampedPosition;
    }

    private void DrawBounds()
    {
        // Draw clamp in editor
        float l = this.bounds.x;
        float r = this.bounds.y;
        float t = this.bounds.z;
        float b = this.bounds.w;

        Debug.DrawLine(new Vector3(l, t, 0), new Vector3(r, t, 0), this.debugLineColour, this.debugLineLife);
        Debug.DrawLine(new Vector3(r, t, 0), new Vector3(r, b, 0), this.debugLineColour, this.debugLineLife);
        Debug.DrawLine(new Vector3(r, b, 0), new Vector3(l, b, 0), this.debugLineColour, this.debugLineLife);
        Debug.DrawLine(new Vector3(l, b, 0), new Vector3(l, t, 0), this.debugLineColour, this.debugLineLife);
    }
}
