using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PushButton : MonoBehaviour
{
    public GameObject startSound;

    IEnumerator IntroScene()
    {
        Debug.Log("기다린다");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("기다렸다");
        SceneManager.LoadScene("Main Game", LoadSceneMode.Single);
    }

    public void OnStartButton()
    {
        AudioSource startSound1 = startSound.GetComponent<AudioSource>();
        startSound1.Play();
        StartCoroutine(IntroScene());
    }
}
