using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerScript : MonoBehaviour
{
    public GameObject attack;
    private GameObject player;

    private float distance;

    private Vector3 player_pos;

    private float attacktimer;

    private Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("release", false);
        player = GameObject.Find("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�Ƃ̋����𑪂�
        player_pos = player.transform.position;
        distance = Mathf.Abs(this.transform.position.x - player_pos.x);

        //������50�ȓ����������A�U�����J�n����
        if (distance < 50)
        {
            attacktimer += Time.deltaTime; //�U���Ԋu���͂���ϐ�
            if (attacktimer > 0.25f)
            {
                anim.SetBool("release", false);
            }
            //��莞�Ԍo�ߌ�A�e(Attack)�𔭎˂���
            if (attacktimer > 5)
            {
                anim.SetBool("release", true);
                Instantiate(attack, this.transform.position, this.transform.rotation);
                attacktimer = 0;
            }
        }            
    }
}
