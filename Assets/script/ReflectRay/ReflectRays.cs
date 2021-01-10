using System.Collections.Generic;
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
        this.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (this.gameObject.tag == "StartLine")
        {
            lr.SetWidth(3.0f, 3.0f);
            ShotRay(new Vector2(-103, 130), Vector2.down, 10000);
        }
        /*else
        {
            Vector2 startPosition = transform.parent.gameObject.GetComponent<BlackFairy>().isPos;
            Vector2 direction = transform.parent.gameObject.GetComponent<BlackFairy>().nowDir;
            float maxDistance = 1000;

            ShotRay(startPosition, direction, maxDistance);

        }*/
    }

    // Physics2D.Raycast(시작위치, 방향, 충돌 반환, 길이값)
    // sendmessage

    public void ShotRay(Vector2 startPosition, Vector2 direction, float maxDistance, int maxReflections = int.MaxValue)
    {
        var hitData = Physics2D.Raycast(startPosition, direction, maxDistance);
        Debug.DrawRay(startPosition, direction * maxDistance, Color.red);

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
        
        if (hitData.collider.gameObject.tag == "BFairy")
        {
            //Debug.Log("검은색 요정");
            hitData.transform.SendMessage("ChangeChildActive");
        }
    }
}