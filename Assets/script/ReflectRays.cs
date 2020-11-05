using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ReflectRays : MonoBehaviour
{

    float currentDistance;
    int currentReflections = 0;
    int maxReflections;
    List<Vector3> Points;
    LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        Points = new List<Vector3>();
        lr = transform.GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if(this.gameObject.tag == "StartLine")
        {
            ShotRay(new Vector2(-18, 40), Vector2.down, 100);
        }
    }

    public void ShotRay(Vector2 startPosition, Vector2 direction, float maxDistance, int maxReflections = int.MaxValue)
    {
        var hitData = Physics2D.Raycast(startPosition, direction, maxDistance);

        this.maxReflections = maxReflections;
        currentDistance = maxDistance;
        
        currentReflections = 0;
        Points.Clear();
        Points.Add(startPosition);

        if (hitData)
        {
            currentDistance -= Vector2.Distance(startPosition, hitData.point);
            ReflectFurther(startPosition, hitData);
        }
        else
        {
            Points.Add(startPosition + direction.normalized * currentDistance);

        }

        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;
        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        if (hitData.collider.gameObject.tag == "Reflect") // 라인렌더러에 닿은 태그 비교
        {
            var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection.normalized, currentDistance);
            // 시작 위치                                                                                               

            if (newHitData)
            {
                currentDistance -= Vector2.Distance(newHitData.point, hitData.point);
                ReflectFurther(hitData.point, newHitData);
            }
            else
            {
                Points.Add(hitData.point + newDirection * currentDistance);
            }
        }
        
        if(hitData.collider.gameObject.tag == "Prism")
        {
            Debug.Log("프리즘에 걸림");
            hitData.collider.GetComponent<PrismReflect>().SplitLaser(hitData.point, origin - hitData.point, currentDistance);
            Points.Add(hitData.point);
        }

    }
}