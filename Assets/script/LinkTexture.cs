using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkTexture : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Texture secondTexture;

    void Start()
    {
        lineRenderer.material.SetTexture("_MainTex", secondTexture);
    }
}
