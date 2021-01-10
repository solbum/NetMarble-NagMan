
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeToNextLevel()
    {
        animator.SetTrigger("FadeToNextLevel");
    }

    public void OnFadeToNextLevelComplete()
    {
        SceneManager.LoadScene("MainGame2");
    }

    public void FadeToStart()
    {
        animator.SetTrigger("LevelEnd");
    }

    public void OnFadeToStartComplete()
    {
        SceneManager.LoadScene("StartScenes");
    }
}
