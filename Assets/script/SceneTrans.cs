using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrans : AutoFlip
{
    private void Start()
    {
        ControledBook.OnFlip.AddListener(new UnityEngine.Events.UnityAction(PageFlipped));
    }
    public void NextScene()
    {
        FlipRightPage();
        StartCoroutine("HideBook");
    }
    public void PrevScene()
    {
        FlipLeftPage();
        StartCoroutine("HideBook");
    }
    IEnumerator HideBook()
    {
        yield return new WaitForSeconds(1);
    }
}

