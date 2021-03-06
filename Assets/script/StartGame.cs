﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    public Image image;
    public Image backGround;

    public GameObject Logo;

    public GameObject isUseUi;

    private void Awake()
    {
        StartCoroutine(VanishImage());
        isUseUi.SetActive(false);
    }



    public IEnumerator VanishImage()
    {
        yield return new WaitForSeconds(2.0f);
        image.CrossFadeAlpha(0f, 2f, false);
        yield return new WaitForSeconds(4.0f);
        isUseUi.SetActive(true);
        LogoControl logo = Logo.GetComponent<LogoControl>();
        logo.LogoStart();
       
    }
}
