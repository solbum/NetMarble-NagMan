﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class ReflectRays : MonoBehaviour
{

    float currentDistance;
    int currentReflections = 0;
    int maxReflections = int.MaxValue;
    
    public bool nowStart = false;

    List<Vector3> Points;

    LineRenderer lr;
    Ray ray;

    // Use this for initialization
    void Awake()
    {
        Points = new List<Vector3>();
        lr = transform.GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (this.gameObject.tag == "StartLine")
        {
            ShotRay(new Vector2(-18, 40), Vector2.down, 100);
        }
        if (nowStart)
        {
            Vector2 startPosition = transform.parent.gameObject.GetComponent<BlackFairy>().isPos;
            Vector2 direction = transform.parent.gameObject.GetComponent<BlackFairy>().nowDir;
            float maxDistance = 1000;

            ShotRay(startPosition, direction, maxDistance);

        }
    }

    // Physics2D.Raycast(시작위치, 방향, 충돌 반환, 길이값)

    public void ShotRay(Vector2 startPosition, Vector2 direction, float maxDistance, int maxReflections = int.MaxValue)
    {
        var hitData = Physics2D.RaycastAll(startPosition, direction, maxDistance);

        for (int i = 0; i < hitData.Length; i++)
        {
            if (hitData[i] != this.transform.parent)
            {
                this.maxReflections = maxReflections;
                currentDistance = maxDistance;

                currentReflections = 0;
                Points.Clear();
                Points.Add(startPosition);


                if (hitData[i])
                {
                    currentDistance -= Vector2.Distance(startPosition, hitData[i].point);
                    ReflectFurther(startPosition, hitData[i]);
                }
                else
                {
                    Points.Add(startPosition + direction.normalized * currentDistance);

                }

                lr.positionCount = Points.Count;
                lr.SetPositions(Points.ToArray());
            }
        }
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

        if (hitData.collider.gameObject.tag == "BFairy")
        {
            Debug.Log("검은색 요정");
            hitData.collider.GetComponent<BlackFairy>().ChangeChildStat();
            
        }
    }
}