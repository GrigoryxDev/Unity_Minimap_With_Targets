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
        Rect RectScreenSpace = RectTransformToScreenSpace(rectangleRect);

        position.x = position.x * RectScreenSpace.width + corners[0].x;

        position.y = position.y * RectScreenSpace.height + corners[0].y;

    }

    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {

        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);

        float x = transform.position.x + transform.anchoredPosition.x;

        float y = Screen.height - transform.position.y - transform.anchoredPosition.y;

        return new Rect(x, y, size.x, size.y);
    }


}