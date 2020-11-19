using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineRay : MonoBehaviour
{
    float currentDistance;
    int currentReflections = 0;
    int maxReflections = int.MaxValue;

    public bool nowStart = false;

    List<Vector3> Points;

    LineRenderer lr;
    Ray ray;

    private void FixedUpdate()
    {
        if (this.gameObject.tag == "StartLine")
        {
            ShotRay(new Vector2(-18, 40), Vector2.down, 100);
        }

    }

    public void ShotRay(Vector2 startPosition, Vector2 direction, float maxDistance, int maxReflections = int.MaxValue)
    {
        var hitData = Physics2D.Raycast(startPosition, direction, maxDistance);

        if (hitData != GameObject.FindWithTag("BFairy"))
        {
            this.maxReflections = maxReflections;
            currentDistance = maxDistance;

            currentReflections = 0;
            Points.Clear();
            Points.Add(startPosition);
        }

        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());

        if (hitData.collider.gameObject.tag == "BFairy")
        {
            Debug.Log("검은색 요정");

        }
    }
}


