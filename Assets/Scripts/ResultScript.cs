using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    public Text LastScoreText;
    public Text HighScoreText;
    float highscore;
    float lastscore;

    int score_int;
    float score_float;
    int score_second = 0;
    int score_minute = 0;

    int highscore_int;
    float highscore_float;
    int highscore_second = 0;
    int highscore_minute = 0;


    public AudioClip buttonSound;
    AudioSource audioSourse;
    private float scenechange = 0.0f;
    private bool reStartChange = false;
    private bool toTitleChange = false;


    void Start()
    {
        
        lastscore = PlayerPrefs.GetFloat("score");
        score_int = (int)lastscore;
        score_float = (lastscore - score_int) * 100;
        score_second = score_int % 60;
        score_minute = score_int / 60;
        LastScoreText.text = score_minute.ToString("00") + ":" + score_second.ToString("00") + "." + score_float.ToString("00");

        if (PlayerPrefs.HasKey("highscore") == true)
        {
            highscore = PlayerPrefs.GetFloat("highscore");
            if (highscore > lastscore)
            {
                highscore = lastscore;
            }
            PlayerPrefs.SetFloat("highscore", highscore);
        }
        else
        {
            highscore = lastscore;
            PlayerPrefs.SetFloat("highscore", highscore);
        }

        highscore_int = (int)highscore;
        highscore_float = (highscore - highscore_int) * 100;
        highscore_second = highscore_int % 60;
        highscore_minute = highscore_int / 60;
        HighScoreText.text = highscore_minute.ToString("00") + ":" + highscore_second.ToString("00") + "." + highscore_float.ToString("00");

        //最高記録リセット用↓↓
        //PlayerPrefs.DeleteKey("highscore");
        //PlayerPrefs.DeleteKey("score");


        audioSourse = this.gameObject.GetComponent<AudioSource>();
        scenechange = 0.0f;
    }

    private void Update()
    {
        if (reStartChange || toTitleChange)
        {
            scenechange += Time.deltaTime;
            if (scenechange > 0.6f && reStartChange)
            {
                SceneManager.LoadScene("Main");
            }
            if (scenechange > 0.6f && toTitleChange)
            {
                SceneManager.LoadScene("Start");
            }
        }
    }


    //再スタートするボタン
    public void RestartButton()
    {
        audioSourse.clip = buttonSound;
        audioSourse.Play();
        reStartChange = true;
    }
    //タイトルに戻るボタン
    public void BackToStartButton()
    {
        audioSourse.clip = buttonSound;
        audioSourse.Play();
        toTitleChange = true;
    }
}
