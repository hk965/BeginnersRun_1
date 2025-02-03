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
        //プレイヤーとの距離を測る
        player_pos = new Vector2(player.transform.position.x, player.transform.position.y);
        distance = Mathf.Abs(transform.position.x - player_pos.x);

        anim.SetBool("Attack_move", true);


        if (playerpos_getter == 0) //プレイヤーの位置を未取得の場合
        {
            //プレイヤーの位置を取得し、プレイヤーの居る方向を計算
            target_pos = player.transform.position;
            target_direction = (target_pos - new Vector2(transform.position.x,transform.position.y)).normalized;
            playerpos_getter = 2;
        }
        if (playerpos_getter == 2) //プレイヤーの位置取得後
        {
            //弾が生成された時点でプレイヤーが居た方向(target_direction)に向かって弾を移動させる
            transform.position += new Vector3(target_direction.x, target_direction.y, 0) * flyspeed * Time.deltaTime;
        }
        //プレイヤーから距離が離れた時、消滅する
        if (distance > 50)
        {
            Destroy(this.gameObject);
        }
    }

}
