using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public Text Score_Text;
    public GameObject Dynamite_UI;
    public Image FadeImage;
    public GameObject ClearGroup;
    public GameObject GameOverGroup;
    public Text FinalScore;
    public bool isFadeEnded;

    int jwels;

    int score;
    int throwweapons;

    void Awake()
    {
        gm = this;

        jwels = 0;

        Score_Text.text  = jwels.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatejewlstake()
    {
        jwels++;
        score = jwels * 100;
        Score_Text.text = score.ToString();
    }

    public void updatedynamitetake()
    {
        Dynamite_UI.SetActive(true);
    }

    public void showGameResult()
    {
        StartCoroutine(FadeIn_Start(2f, true));

        FinalScore.text = score.ToString();

        CanvasGroup clearGroup = GameObject.FindWithTag("ClearGroup").GetComponent<CanvasGroup>();

        clearGroup.alpha = 1f;

        clearGroup.interactable = true;
        clearGroup.blocksRaycasts = true;
        clearGroup.ignoreParentGroups = true;
    }

    public void showGameOver()
    {
        StartCoroutine(FadeIn_Start(2f, true));

        CanvasGroup overGroup = GameObject.FindWithTag("GameOverGroup").GetComponent<CanvasGroup>();

        overGroup.alpha = 1f;

        overGroup.interactable = true;
        overGroup.blocksRaycasts = true;
        overGroup.ignoreParentGroups = true;
    }

    IEnumerator FadeIn_Start(float fadeTime, bool isFadeEnded)
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

        yield return new WaitForSeconds(1f);

        isFadeEnded = false;
    }
}
