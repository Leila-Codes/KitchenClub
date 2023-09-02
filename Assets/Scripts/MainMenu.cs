using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator characterAnimator;
    public GameObject blackoutScreen;
    public GameObject cutsceneTimeline;
    
    private static readonly int Waving = Animator.StringToHash("waving");
    
    public void StartGame()
    {
        cutsceneTimeline.SetActive(true);
        StartCoroutine(ActuallyPlay());
    }
    
    public void ExitGame()
    {
        characterAnimator.SetBool(Waving, true);
        StartCoroutine(ActuallyQuit());
    }

    IEnumerator ActuallyQuit()
    {
        yield return new WaitForSeconds(1.5f);
        blackoutScreen.SetActive(true);
        Application.Quit();
        yield return new WaitForSeconds(1);
        blackoutScreen.SetActive(true);
    }

    IEnumerator ActuallyPlay()
    {
        yield return new WaitForSeconds(4.5f);
        
        SceneManager.LoadScene("Day01");
    }
}