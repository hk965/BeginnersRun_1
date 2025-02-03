using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class HowToScript : MonoBehaviour
{
    public AudioClip DecideSound;
    AudioSource audioSourse;
    private float scenechange = 0.0f;
    private bool scenechanger = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSourse = this.gameObject.GetComponent<AudioSource>();
        scenechange = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (scenechanger)
        {
            scenechange += Time.deltaTime;
            if (scenechange > 0.6f)
            {
                SceneManager.LoadScene("Skill");
            }
        }
    }

    //スキル選択画面に移動するボタン
    public void DecideButton()
    {
        audioSourse.clip = DecideSound;
        audioSourse.Play();
        scenechanger = true;
    }
}
