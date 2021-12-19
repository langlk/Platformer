using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    const float padding = .015f;
    BoxCollider2D boxCollider;
    RaycastOrigins raycastOrigins;
    float horizontalSpacing;
    float verticalSpacing;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        DrawRays(verticalRayCount, verticalSpacing, raycastOrigins.bottomLeft, Vector2.right, Vector2.down);
        DrawRays(horizontalRayCount, horizontalSpacing, raycastOrigins.topRight, Vector2.down, Vector2.right);
    }

    void UpdateRaycastOrigins() {
        Bounds bounds = GetBounds();

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing() {
        Bounds bounds = GetBounds();
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        horizontalSpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalSpacing = bounds.size.x / (verticalRayCount - 1);
    }

    Bounds GetBounds() {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(padding * -2);
        return bounds;
    }

    void DrawRays(int count, float spacing, Vector2 origin, Vector2 moveDirection, Vector2 rayDirection) {
        for (int i = 0; i < count; i++) {
            Debug.DrawRay(origin + moveDirection * spacing * i, rayDirection * 2, Color.red);
        }
    }

    struct RaycastOrigins {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }
}
