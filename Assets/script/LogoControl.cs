using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoControl : MonoBehaviour
{
    public Text ChatText;

    public string writerText = "";

    public void LogoStart()
    {
        Debug.Log("시작");
        StartCoroutine(TextPractice());    
    }

    IEnumerator NormalLogo(string narration)
    {
        Debug.Log("출력");
        int a = 0;
        writerText = "";
        for(a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator TextPractice()
    {
        Debug.Log("연습");
        yield return StartCoroutine(NormalLogo("낭만여행가"));
    }
}
