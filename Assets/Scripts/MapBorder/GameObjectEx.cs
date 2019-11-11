using UnityEngine;

public static class GameObjectEx
{
    public static void DrawCircle(this GameObject container, float radius, float lineWidth)
    {
        var segments = 360;
        var line = container.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        float colorSeter = 3f;
        line.material.color = new Color(colorSeter,colorSeter,colorSeter,1);
        
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;


        

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }
}


