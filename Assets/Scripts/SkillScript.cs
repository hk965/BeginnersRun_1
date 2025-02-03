using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SkillScript : MonoBehaviour
{
    //スキル選択画面で使うボタンを召喚。楽にスキルを増やせる
    public Button[] buttons = new Button[4];

    public AudioClip decideSound;
    public AudioClip skillchoiseSound;
    AudioSource audioSourse;
    private float scenechange = 0.0f;
    private bool scenechanger = false;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("skill");　//スキルを選択していない状態へ
        buttons[0].interactable = false;  //スキル選択前は決定ボタン押せない

        audioSourse = this.gameObject.GetComponent<AudioSource>();
        scenechange = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //スキルを選択したら決定ボタンが押せるようになる
        if (PlayerPrefs.HasKey("skill") == true)
        {
            buttons[0].interactable = true;
        }

        if (scenechanger)
        {
            scenechange += Time.deltaTime;
            if (scenechange > 0.6f)
            {
                SceneManager.LoadScene("Stage");
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

    //スキル選択についての関数(スキルのボタンを押すと呼び出される)
    public void Skill(int i)
    {
        PlayerPrefs.SetInt("skill", i);  //各スキルに番号を割り振り、ボタンを押すとその番号を返すので、何番のスキルを選択したかわかる
        for (int j = 1; j < buttons.Length; j++)
        {
            buttons[j].image.color = new Color32(255, 255, 255, 255);
        }
        buttons[i].image.color = new Color32(255, 150, 0, 255);

        audioSourse.clip = skillchoiseSound;
        audioSourse.Play();
    }
}
