using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageScript : MonoBehaviour
{
    //スキル選択画面で使うボタンを召喚。楽にスキルを増やせる
    public Button[] buttons;

    public AudioClip decideSound;
    public AudioClip stagechoiseSound;
    AudioSource audioSourse;
    private float scenechange = 0.0f;
    private bool scenechanger = false;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("stage");　//ステージを選択していない状態へ
        buttons[0].interactable = false;  //ステージ選択前は決定ボタン押せない

        audioSourse = this.gameObject.GetComponent<AudioSource>();
        scenechange = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //スキルを選択したら決定ボタンが押せるようになる
        if (PlayerPrefs.HasKey("stage") == true)
        {
            buttons[0].interactable = true;
        }

        if (scenechanger)
        {
            scenechange += Time.deltaTime;
            if (scenechange > 0.6f)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    //メイン画面に移動するボタン
    public void DecideButton()
    {
        audioSourse.clip = decideSound;
        audioSourse.Play();
        scenechanger = true;
    }

    //ステージ選択についての関数
    public void Stage(int i)
    {
        PlayerPrefs.SetInt("stage", i);  //各ステージに番号を割り振っているので、何番のスキルを選択したかわかる
        for (int j = 1; j < buttons.Length; j++)
        {
            buttons[j].image.color = new Color32(255, 255, 255, 255);
        }
        buttons[i].image.color = new Color32(255, 150, 0, 255);

        audioSourse.clip = stagechoiseSound;
        audioSourse.Play();
    }
}
