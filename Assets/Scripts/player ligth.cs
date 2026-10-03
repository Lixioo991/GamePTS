using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Transform light;
    public float smoothTime = 0.25f;

    Vector2 currentDirection;
    Vector2 directionVelocity;

    void Update()
    {
        Vector2 targetDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (targetDirection != Vector2.zero)
        {
            targetDirection.Normalize();

            currentDirection = Vector2.SmoothDamp(
                currentDirection,
                targetDirection,
                ref directionVelocity,
                smoothTime
            );

            float angle = Mathf.Atan2(
                currentDirection.y,
                currentDirection.x
            ) * Mathf.Rad2Deg - 90f;

            light.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}