using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleManager : MonoBehaviour
{
    public Animator animator;

    public void quit()
    {
        Application.Quit();
    }

    public void transitionToEnd()
    {
        Invoke("enterLevel", 1);
        animator.SetTrigger("FadeOut");
    }

    public void enterLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
