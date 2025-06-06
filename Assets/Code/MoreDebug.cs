using UnityEngine;

public abstract class MoreDebug : Debug {
    public static void DrawCircle(Vector3 position, float radius, int segments, Color color) {
        // If either radius or number of segments are less or equal to 0, skip drawing
        if (radius <= 0.0f || segments <= 0) return;


        // Single segment of the circle covers (360 / number of segments) degrees
        var angleStep = 360.0f / segments;

        // Result is multiplied by Mathf.Deg2Rad constant which transforms degrees to radians
        // which are required by Unity's Mathf class trigonometry methods

        angleStep *= Mathf.Deg2Rad;

        // lineStart and lineEnd variables are declared outside of the following for loop
        var lineStart = Vector3.zero;
        var lineEnd   = Vector3.zero;

        for (var i = 0; i < segments; i++) {
            // Line start is defined as starting angle of the current segment (i)
            lineStart.x = Mathf.Cos(angleStep * i);
            lineStart.z = Mathf.Sin(angleStep * i);

            // Line end is defined by the angle of the next segment (i+1)
            lineEnd.x = Mathf.Cos(angleStep * (i + 1));
            lineEnd.z = Mathf.Sin(angleStep * (i + 1));

            // Results are multiplied so they match the desired radius
            lineStart *= radius;
            lineEnd   *= radius;

            // Results are offset by the desired position/origin 
            lineStart += position;
            lineEnd   += position;

            lineStart.y = position.y;
            lineEnd.y   = position.y;

            // Points are connected using DrawLine method and using the passed color
            DrawLine(lineStart, lineEnd, color);
        }
    }
}