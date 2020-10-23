using UnityEngine;

public static class MathUtilities
{


    public static Vector3 RandomPointInAnnulus(Vector3 origin, float minRadius, float maxRadius)
    {

        var randomInCircle = Random.insideUnitCircle.normalized;
        Vector3 randomDirection = new Vector3(randomInCircle.x, 0, randomInCircle.y);
        var randomDistance = Random.Range(minRadius, maxRadius);

        Vector3 point = origin + randomDirection * randomDistance;

        return point;
    }

    public static void ClampPositionToCircle(Vector2 center, float radius, ref Vector2 position)
    {
        // Calculate the offset vector from the center of the circle to our position
        Vector2 diff = position - center;
        // Calculate the linear distance of this offset vector
        float distance = diff.magnitude;

        if (radius < distance)
        {
            Vector2 direction = diff / distance;
            position = center + direction * radius;
        }
    }

    public static void ClampPositionToRectRectangle(RectTransform rectangleRect, ref Vector2 position)
    {
        var corners = new Vector3[4];
        rectangleRect.GetWorldCorners(corners);

        position.x = Mathf.Clamp(position.x * rectangleRect.rect.width + corners[0].x, corners[0].x, corners[2].x);
        position.y = Mathf.Clamp(position.y * rectangleRect.rect.height + corners[0].y, corners[0].y, corners[1].y);
    }

}