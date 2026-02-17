using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Update the last point to current position
        if (points.Count == 0)
        {
            points.Add(transform.position);
            points.Add(transform.position);
        }
        else
        {
            points[points.Count - 1] = transform.position;
        }

        // Optional: Add new points based on distance moved for better performance
        // if (Vector3.Distance(points[points.Count-2], transform.position) > 0.5f) { ... }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
