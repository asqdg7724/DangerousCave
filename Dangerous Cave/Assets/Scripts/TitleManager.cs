using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TitleSound SoundPlayer;
    public Image FadeImage;
    public Text myText;
    bool isFadeEnded;

    // Start is called before the first frame update
    void Start()
    {
        isFadeEnded = false;

        StartCoroutine(FadeOut(2f, true));
        StartCoroutine(blinkText(1f));
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SoundPlayer.SoundPlay(0);
            StartCoroutine(FadeIn(2f, true));
        }
    }

    IEnumerator blinkText(float blinkTime)
    {

        while (true)
        {
            myText.enabled = false;
            yield return new WaitForSeconds(blinkTime);
            myText.enabled = true;
            yield return new WaitForSeconds(blinkTime);
        }
    }

    IEnumerator FadeOut(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        while (t < fadeTime)
        {
            //Update t value.
            t += Time.deltaTime;

            //Calculate 진행%.
            float percent = t / fadeTime;      //Mathf.Clamp()

            //Update 투명도.
            if (isFadeEnded)
                FadeImage.color = new Color(FadeImage.color.r,
                                            FadeImage.color.g,
                                            FadeImage.color.b,
                                            Mathf.Lerp(1f, 0, percent));

            yield return null;
        }

        isFadeEnded = false;
    }

    IEnumerator FadeIn(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            float percent = t / fadeTime;

            if (isFadeEnded)
                FadeImage.color = new Color(FadeImage.color.r,
                                            FadeImage.color.g,
                                            FadeImage.color.b,
                                            Mathf.Lerp(0, 1f, percent));
            yield return null;
        }

        isFadeEnded = false;
        SceneManager.LoadScene("Training_Stage");
    }

}
