using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismReflect : MonoBehaviour
{
    ReflectRays[] laser;
    Vector2[] pos;
    Vector2 poi;

    public int confirmObject;

    [SerializeField] [Range(0, 90)]
    float maxAngle;

    private void Awake()
    {
        laser = transform.GetComponentsInChildren<ReflectRays>();
    }

    private void FinePos()
    {
        switch(confirmObject)
        {
            case 1 :
                pos[confirmObject] = this.gameObject.transform.position;
                poi = new Vector2(-29, 14);
                break;
            case 2:
                pos[confirmObject] = this.gameObject.transform.position;
                poi = new Vector2(-17, 4);
                break;
        }
        SplitLaser(pos[confirmObject], poi.normalized, 100);
        Debug.Log(pos);
    }

    public void SplitLaser(Vector2 hitPosition, Vector2 direction, float currentDistance)
    {
        float angle = maxAngle / (laser.Length - 1);

        for (int i = 0; i < laser.Length; i++)
        {
            //Quaternion rot = Quaternion.AngleAxis(angle * i + 20, Vector3.forward);
            //Vector2 dir = rot * direction;

            //laser[i].ShotRay(hitPosition, dir.normalized, currentDistance);
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag != "Player")
        {
            FinePos();
        }
    }
}
