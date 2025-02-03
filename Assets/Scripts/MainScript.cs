using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    int skill;
    float countdown = 3.0f;
    int count;
    float timer = 0.00f;
    int timer_int;
    float timer_float;
    int second = 0;
    int minute = 0;
    
    public Text starttext;
    public Text timertext;
    public Text skilltext;

    public Button[] playerManager = new Button[4];

    bool isPlaying = false;

    public float originalspeed = 12;
    public float originaljumpPower = 21;

    float speed;
    float jumpPower;
    float ySpeed;
    bool isJump = false;
    int jumpCount = 0;
    private float jumpPos = 0.0f;
    public float jumpHeight;
    public float jumpLimitTime;
    private float jumpTime = 0.0f;

    bool leftMove;
    bool rightMove;
   
    int pushright = 0; //0=値無し、1=true、2=false
    int pushleft = 0; //0=値無し、1=true、2=false
    bool pushjump = false;

    bool skillnow = false;
    float skilltimer = 0.0f;
    float cooltime = 10.0f;

    public GroundChecker ground;
    public GroundChecker head;
    bool isHead = false;
    bool isGround = true;
    public RightChecker right;
    bool isRight = false;
    public LeftChecker left;
    bool isLeft = false;
    public GoalChecker goal1;
    public GoalChecker goal2;
    bool isGoal1 = false;
    bool isGoal2 = false;

    bool slower = false;
    bool onEnemy = false;
    float slowertimer = 0.0f;

    bool muteki = false;
    bool speedUp = false;
    bool secondjump = false;

    public float speedUpRate = 1.5f;

    private Animator anim = null;
    private Rigidbody2D rb = null;
    private MoveObject moveObj;
    private SpriteRenderer ren;

    public AudioClip jumpSound;
    bool jumpSoundOn = false;
    bool skillSoundOn = false;
    bool damageSoundOn = false;
    bool goalSoundOn = true;
    public AudioClip skillSound;
    public AudioClip damageSound;
    public AudioClip gameStartSound;
    public AudioClip goalSound;
    AudioSource audioSourse;

    int stage_num;

    // Start is called before the first frame update
    void Start()
    {
        //選んだステージによってスタートの座標が変わる
        stage_num = PlayerPrefs.GetInt("stage");
        if (stage_num == 1)
        {
            this.transform.position = new Vector3(-20.2f, -17.69f, -0.32f);
        }
        else if (stage_num == 2)
        {
            this.transform.position = new Vector3(1377.19f, -28.74836f, -0.32f);
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ren = gameObject.GetComponent<SpriteRenderer>();
        audioSourse = this.gameObject.GetComponent<AudioSource>();

        starttext.text = "touch the screen!";
        skilltext.text = "SKILL";
        for(int i = 0; i < playerManager.Length; i++)
        {
            playerManager[i].interactable = false;
        }

        speed = originalspeed;
        jumpPower = originaljumpPower;

        audioSourse.clip = gameStartSound;

        //選んだスキルを"スキル発動ボタン"に適応させる
        skill = PlayerPrefs.GetInt("skill");
        playerManager[0].onClick.AddListener(() => SkillMaker(skill));
    }

    private void FixedUpdate()
    {
        //接地判定
        if (countdown <= 0 && countdown >= -10)
        {
            isHead = head.IsGround();
            isGround = ground.IsGround();
            isRight = right.IsRight();
            isLeft = left.IsLeft();
            isGoal1 = goal1.IsGoal();
            isGoal2 = goal2.IsGoal();

        }

        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);

    }
    // Update is called once per frame
    void Update()
    {
        if (isPlaying == false)
        {
            //画面をタップ(画面をマウスでクリック)するまでゲーム開始しない
            if (Input.GetMouseButtonDown(0) == true)
            {
                audioSourse.Play();
                isPlaying = true;
            }
        }
        if (isPlaying == true)
        {
            //ゲーム開始のカウントダウン
            countdown -= Time.deltaTime;
            count = (int)countdown + 1;
            starttext.text = count.ToString();

            if (isGoal1 || isGoal2)
            {
                starttext.text = "GOAL!!";
            }

            //ゲームの動作本編
            if (countdown <= 0 && countdown >= -5)
            {
                //タイムの計測開始およびタイムの表示
                if (isGoal1 == false && isGoal2 == false)
                {
                    countdown = 0;
                    timer += Time.deltaTime;
                }
                timer_int = (int)timer;
                timer_float = (timer - timer_int) * 100;
                second = timer_int % 60;
                minute = timer_int / 60;                
                timertext.text = minute.ToString("00") + ":" + second.ToString("00") + "." + timer_float.ToString("00");
                if (minute > 59 && second > 59 && timer_float > 99)
                {
                    timer = 3599.99f;
                    isGoal1 = true;
                    Goal();
                }

                //カウントダウンしていたテキストを消す
                starttext.text = "start!!";
                if (timer > 1)
                {
                    starttext.text = "";
                }

                //プレイヤーの動作について
                //ゲーム開始直後、全てのボタンをオンにする
                for (int i = 0; i < playerManager.Length; i++)
                {
                    playerManager[i].interactable = true;
                }
                //何かとぶつかったときの挙動
                Collision();
                //左右、ジャンプ、スキルの動作Move
                RightLeft_Manager();
                Move();
                //スキル発動時のスキルボタンの挙動
                if (skillnow == true)
                {
                    SkillButtonCoolTime();
                }
                //デバフ解除の処理
                if (slower == true)
                {
                    EliminateSlower();
                }

                //ゴール
                Goal();

            }
            else if (countdown < -5)   //リザルト画面へシーン遷移
            {
                SceneManager.LoadScene("Result");
            }
        }
    }

    //ゴール時の処理
    void Goal()
    {
        if (isGoal1 == true || isGoal2 == true)
        {
            if (goalSoundOn)
            {
                audioSourse.clip = goalSound;
                audioSourse.Play();
                goalSoundOn = false;
            }

            timer = timer + 0.0f;
            countdown -= Time.deltaTime;
            for (int k = 0; k < playerManager.Length; k++)
            {
                playerManager[k].interactable = false;
            }
            starttext.text = "GOAL!!";
            rightMove = true;
            anim.SetBool("run", true);
            PlayerPrefs.SetFloat("score", timer);
        }
    }

    //スキルボタンを押したかどうかの判定
    public void OnClickSkillButton()
    {
        skillnow = true;
        skillSoundOn = true;
    }

    //スキル発動から再発動までのクールタイムとスキルボタンの挙動
    void SkillButtonCoolTime()
    {
        //スキルボタンの使用不可にする
        playerManager[0].interactable = false;
        //スキルの効果時間中であることを明記する
        skilltext.text = "発動中！";
        //スキル発動時間をカウントする
        skilltimer += Time.deltaTime;
        //10秒後、スキルが終了し、クールタイムに入る
        if (skilltimer > 10)
        {
            //スキル発動時に変更したフラグを戻す
            speed = originalspeed;
            muteki = false;
            speedUp = false;
            secondjump = false;
            //クールタイムをカウントする
            cooltime -= Time.deltaTime;
            //クールタイムはスキルボタン上に表示する
            skilltext.text = cooltime.ToString("f2");
            //クールタイムが終わったらスキルを再使用できるようにする
            if (cooltime < 0)
            {
                //スキルボタンを使用可能にする
                playerManager[0].interactable = true;
                //スキルボタンの表記を戻す
                skilltext.text = "SKILL";
                //クールタイムの処理に関する変数を初期化
                skillnow = false;
                skilltimer = 0.0f;
                cooltime = 10.0f;
            }
        }
    }

    //スキルの内容
    void SkillMaker(int num)
    {
        if (num == 1)//10秒間スピードアップ
        {
            speed = originalspeed * speedUpRate;
            speedUp = true;
        }
        if (num == 2)//10秒間ジャンプ性能アップ
        {
            secondjump = true;
        }
        if (num == 3)//10秒間無敵
        {
            muteki = true;
            //↓今受けているデバフを完全回復
            ren.color = new Color(1f, 1f, 1f, 1f);
            speed = originalspeed;
            slower = false;
            slowertimer = 0.0f;
        }
    }

    //ジャンプについて
    public void Jump()
    {
        bool canTime = jumpLimitTime > jumpTime;
        if (isGround)
        {
            //地上からジャンプしたとき
            if (pushjump == true)
            {
                ySpeed = jumpPower;
                jumpPos = transform.position.y;
                isJump = true;
                jumpCount = 1;
            }
            else
            {
                isJump = false;
                ySpeed = 0.0f;
            }
        }
        //ジャンプ中の制御
        else if (isJump)
        {
            //ジャンプの高度制限、滞空時間を設ける
            if (pushjump == true && jumpPos + jumpHeight > transform.position.y && canTime)  //高度制限内、滞空時間内では上昇
            {
                ySpeed = jumpPower;
                jumpTime += Time.deltaTime;
            }
            else  //高度制限もしくは滞空時間を超えると上昇しない
            {
                isJump = false;
                jumpTime = 0.0f;
                ySpeed = 0.0f;
                pushjump = false;
            }
        }
        //空中でプレイヤーが上昇していないとき、かつ、(2段ジャンプのスキル未発動または2段ジャンプ発動中に2段ジャンプ済み)のときにジャンプしようとしたとき
        else if (pushjump && !isGround && !isJump && (!secondjump || (secondjump && jumpCount == 2)))
        {
            //ジャンプボタンを押してもジャンプしない
            jumpSoundOn = false;
            pushjump = false;
        }
        else if (secondjump == true && pushjump == true)
        {
            if (jumpCount == 1)
            {
                jumpTime = 0.0f;
                ySpeed = jumpPower;
                jumpPos = transform.position.y;
                isJump = true;
                jumpCount = 2;
            }
        }

        if (isHead == true)
        {
            isJump = false;
            jumpTime = 0.0f;
            ySpeed = 0.0f;
            pushjump = false;
        }

        JumpSound();
    }

    //左右の移動をボタンと関連付けるための段取り
    public void LeftButtonDown()
    {
        pushleft = 1;
    }
    public void LeftButtonUp()
    {
        pushleft = 2;
    }
    public void RightButtonDown()
    {
        pushright = 1;
    }
    public void RightButtonUp()
    {
        pushright = 2;
    }
    public void JumpButtonDown()
    {
        pushjump = true;
        jumpSoundOn = true;
    }
    public void JumpButtonUp()
    {
        pushjump = false;
    }

    //左右の移動のフラグ
    void RightLeft_Manager()
    {
        if ((Input.GetKey("right") == true || pushright == 1) && (Input.GetKey("left") == true || pushleft == 1))
        {
            anim.SetBool("run", false);
            leftMove = false;
            rightMove = false;
        }
        else if (Input.GetKey("left") == true || pushleft == 1)
        {
            leftMove = true;
            transform.localScale = new Vector3(-1, 1, 1) * 4.08122f;
            anim.SetBool("run", true);
        }
        else if (Input.GetKey("right") == true || pushright == 1)
        {
            rightMove = true;
            transform.localScale = new Vector3(1, 1, 1) * 4.08122f;
            anim.SetBool("run", true);
        }

        if (Input.GetKeyUp("right") == true || isRight == true) 
        {
            rightMove = false;
            anim.SetBool("run", false);
        }
        if (Input.GetKeyUp("left") == true || isLeft == true) 
        {
            leftMove = false;
            anim.SetBool("run", false);
        }

        if (pushright == 2) 
        {
            rightMove = false;
            anim.SetBool("run", false);
            pushright = 0;
        }
        if (pushleft == 2)
        {
            leftMove = false;
            anim.SetBool("run", false);
            pushleft = 0;
        }
    }

    //移動とジャンプ
    void Move()
    {
        float xSpeed_right = 0.0f;
        float xSpeed_left = 0.0f;
        Vector2 myGravity = new Vector2(0, 0);

        //右へ移動するときの挙動
        if (rightMove == true)
        {
            xSpeed_right = speed;
        }
        else if (rightMove == false)
        {
            xSpeed_right = 0.0f;
        }
        //左へ移動するときの挙動
        if (leftMove == true)
        {
            xSpeed_left = -speed;
        }
        else if (leftMove == false)
        {
            xSpeed_left = 0.0f;
        }
        //落下の挙動
        if (isJump == false && isGround == false)
        {
            myGravity = new Vector2(0, -jumpPower);
        }
        
        //PCでのバック用：スペースキーでジャンプするように割り当て
        if (Input.GetKeyDown("space"))
        {
            pushjump = true;
            jumpSoundOn = true;
        }
        else if (Input.GetKeyUp("space"))
        {
            pushjump = false;
        }
        
        //ジャンプ時の挙動
        Jump();

        //動く床に乗ったときのプレイヤーの速度
        Vector2 addVelocity = Vector2.zero;
        if (moveObj != null)
        {
            addVelocity = moveObj.GetVelocity();
        }
        rb.velocity = new Vector2(xSpeed_left + xSpeed_right, ySpeed) + myGravity + addVelocity;

        //PCでのバック用：Sキーでスキル発動するように割り当て
        if (Input.GetKeyDown("s"))
        {
            skillnow = true;
            skillSoundOn = true;
            SkillMaker(skill);
        }
        SkillSound();
    }

    //何かとぶつかったとき
    void Collision()
    {
        //着地
        if (isGround)
        {
            jumpCount = 0;
        }
        //空中にいるとき
        else if (isGround == false && jumpCount == 0)
        {
            jumpCount = 1;
        }
        //デバフ
        if (onEnemy)
        {
            if (!muteki)
            {
                slower = true;
                Slower();
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //障害物に当たったとき
        if (slower == false && col.gameObject.tag == "Enemy")
        {
            if (muteki == false)
            {
                slower = true;
                slowertimer = 0.0f;
            }
            if (muteki == true)
            {
                slower = false;
            }
        }
        if (col.gameObject.tag == "Enemy")
        {
            onEnemy = true;
        }

        //敵の攻撃に当たったとき
        if (slower == false && col.gameObject.tag == "Attack")
        {
            if (muteki == false)
            {
                slower = true;
                Slower();
                slowertimer = 0.0f;
            }
            if (muteki == true)
            {
                slower = false;
            }
        }
        if (col.gameObject.tag == "Attack")
        {

            Destroy(col.gameObject);
        }

        //動く床に乗ったとき
        if (col.gameObject.tag == "Movefloor")
        {
            moveObj = col.gameObject.GetComponent<MoveObject>();
        }

    }

    //何かから離れた時
    private void OnCollisionExit2D(Collision2D col)
    {
        //動く床から離れた
        if (col.gameObject.tag == "Movefloor")
        {
            moveObj = null;
        }

        //Enemyから離れた
        if (col.gameObject.tag == "Enemy")
        {
            onEnemy = false;
        }
    }

    //デバフ
    void Slower()
    {
        if (speedUp == false)
        {
            speed = originalspeed * 0.75f;
        }
        if (speedUp == true)
        {
            speed = originalspeed;
        }
    }

    //デバフ解除の処理
    void EliminateSlower()
    {
        if (slowertimer == 0.0f)
        {
            damageSoundOn = true;
        }
        slowertimer += Time.deltaTime;
        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
        ren.color = new Color(1f, 1f, 1f, level);

        if (damageSoundOn)
        {
            audioSourse.clip = damageSound;
            audioSourse.Play();
            damageSoundOn = false;
        }
        if (slowertimer > 5)
        {
            if (speedUp == false)
            {
                speed = originalspeed;                
            }
            if (speedUp == true)
            {
                speed = originalspeed * speedUpRate;
            }

            ren.color = new Color(1f, 1f, 1f, 1f);

            slower = false;
            slowertimer = 0.0f;
        }
    }

    void JumpSound()
    {
        if (jumpSoundOn)
        {
            audioSourse.clip = jumpSound;
            audioSourse.Play();
            jumpSoundOn = false;
        }
    }

    void SkillSound()
    {
        if (skillSoundOn)
        {
            audioSourse.clip = skillSound;
            audioSourse.Play();
            skillSoundOn = false;
        }
    }
}
