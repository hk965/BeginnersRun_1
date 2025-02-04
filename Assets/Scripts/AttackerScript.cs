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
        //プレイヤーとの距離を測る
        player_pos = player.transform.position;
        distance = Mathf.Abs(this.transform.position.x - player_pos.x);

        //距離が50以内だった時、攻撃を開始する
        if (distance < 50)
        {
            attacktimer += Time.deltaTime; //攻撃間隔をはかる変数
            if (attacktimer > 0.25f)
            {
                anim.SetBool("release", false);
            }
            //一定時間経過後、弾(Attack)を発射する
            if (attacktimer > 5)
            {
                anim.SetBool("release", true);
                Instantiate(attack, this.transform.position, this.transform.rotation);
                attacktimer = 0;
            }
        }            
    }
}
