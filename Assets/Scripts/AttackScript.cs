using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Vector2 target_pos;
    Vector2 target_direction;

    public float flyspeed = 12.0f;

    private GameObject player;
    
    int playerpos_getter = 0;

    private float distance;
    private Vector2 player_pos;

    private Animator anim = null;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    } 

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�Ƃ̋����𑪂�
        player_pos = new Vector2(player.transform.position.x, player.transform.position.y);
        distance = Mathf.Abs(transform.position.x - player_pos.x);

        anim.SetBool("Attack_move", true);


        if (playerpos_getter == 0) //�v���C���[�̈ʒu�𖢎擾�̏ꍇ
        {
            //�v���C���[�̈ʒu���擾���A�v���C���[�̋���������v�Z
            target_pos = player.transform.position;
            target_direction = (target_pos - new Vector2(transform.position.x,transform.position.y)).normalized;
            playerpos_getter = 2;
        }
        if (playerpos_getter == 2) //�v���C���[�̈ʒu�擾��
        {
            //�e���������ꂽ���_�Ńv���C���[����������(target_direction)�Ɍ������Ēe���ړ�������
            transform.position += new Vector3(target_direction.x, target_direction.y, 0) * flyspeed * Time.deltaTime;
        }
        //�v���C���[���狗�������ꂽ���A���ł���
        if (distance > 50)
        {
            Destroy(this.gameObject);
        }
    }

}
