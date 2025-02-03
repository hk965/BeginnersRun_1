using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartScript : MonoBehaviour
{
    public GameObject player;
    private Animator anim = null;

    public AudioClip StartSound;
    AudioSource audioSourse;
    private float scenechange = 0.0f;
    private bool scenechanger = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.gameObject.GetComponent<Animator>();
        audioSourse = this.gameObject.GetComponent<AudioSource>();
        scenechange = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("run", true);
        anim.SetBool("ground", true);
        anim.SetBool("jump", false);
        player.transform.position += new Vector3(12 * Time.deltaTime, 0, 0);
        if (player.transform.position.x > 1204.01f)
        {
            player.transform.position = new Vector3(1156, -17.68995f, -0.32f);
        }

        if (scenechanger)
        {
            scenechange += Time.deltaTime;
            if (scenechange > 0.6f)
            {
                SceneManager.LoadScene("HowToPlay");
            }
        }
    }

    //スキル選択画面に移動するボタン
    public void StartButton()
    {
        audioSourse.clip = StartSound;
        audioSourse.Play();
        scenechanger = true;
    }
}
