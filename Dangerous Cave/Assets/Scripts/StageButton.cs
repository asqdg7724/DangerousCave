using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void GoTrainingStage()
    {
        SceneManager.LoadScene("Training_Stage");
    }

    public void GoStage1()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void GoStage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void GoStage3()
    {
        SceneManager.LoadScene("Stage3");
    }
}
