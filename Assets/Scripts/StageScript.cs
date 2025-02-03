using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageScript : MonoBehaviour
{
    //�X�L���I����ʂŎg���{�^���������B�y�ɃX�L���𑝂₹��
    public Button[] buttons;

    public AudioClip decideSound;
    public AudioClip stagechoiseSound;
    AudioSource audioSourse;
    private float scenechange = 0.0f;
    private bool scenechanger = false;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("stage");�@//�X�e�[�W��I�����Ă��Ȃ���Ԃ�
        buttons[0].interactable = false;  //�X�e�[�W�I��O�͌���{�^�������Ȃ�

        audioSourse = this.gameObject.GetComponent<AudioSource>();
        scenechange = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //�X�L����I�������猈��{�^����������悤�ɂȂ�
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

    //���C����ʂɈړ�����{�^��
    public void DecideButton()
    {
        audioSourse.clip = decideSound;
        audioSourse.Play();
        scenechanger = true;
    }

    //�X�e�[�W�I���ɂ��Ă̊֐�
    public void Stage(int i)
    {
        PlayerPrefs.SetInt("stage", i);  //�e�X�e�[�W�ɔԍ�������U���Ă���̂ŁA���Ԃ̃X�L����I���������킩��
        for (int j = 1; j < buttons.Length; j++)
        {
            buttons[j].image.color = new Color32(255, 255, 255, 255);
        }
        buttons[i].image.color = new Color32(255, 150, 0, 255);

        audioSourse.clip = stagechoiseSound;
        audioSourse.Play();
    }
}
