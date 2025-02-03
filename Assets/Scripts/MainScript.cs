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
   
    int pushright = 0; //0=�l�����A1=true�A2=false
    int pushleft = 0; //0=�l�����A1=true�A2=false
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
        //�I�񂾃X�e�[�W�ɂ���ăX�^�[�g�̍��W���ς��
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

        //�I�񂾃X�L����"�X�L�������{�^��"�ɓK��������
        skill = PlayerPrefs.GetInt("skill");
        playerManager[0].onClick.AddListener(() => SkillMaker(skill));
    }

    private void FixedUpdate()
    {
        //�ڒn����
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
            //��ʂ��^�b�v(��ʂ��}�E�X�ŃN���b�N)����܂ŃQ�[���J�n���Ȃ�
            if (Input.GetMouseButtonDown(0) == true)
            {
                audioSourse.Play();
                isPlaying = true;
            }
        }
        if (isPlaying == true)
        {
            //�Q�[���J�n�̃J�E���g�_�E��
            countdown -= Time.deltaTime;
            count = (int)countdown + 1;
            starttext.text = count.ToString();

            if (isGoal1 || isGoal2)
            {
                starttext.text = "GOAL!!";
            }

            //�Q�[���̓���{��
            if (countdown <= 0 && countdown >= -5)
            {
                //�^�C���̌v���J�n����у^�C���̕\��
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

                //�J�E���g�_�E�����Ă����e�L�X�g������
                starttext.text = "start!!";
                if (timer > 1)
                {
                    starttext.text = "";
                }

                //�v���C���[�̓���ɂ���
                //�Q�[���J�n����A�S�Ẵ{�^�����I���ɂ���
                for (int i = 0; i < playerManager.Length; i++)
                {
                    playerManager[i].interactable = true;
                }
                //�����ƂԂ������Ƃ��̋���
                Collision();
                //���E�A�W�����v�A�X�L���̓���Move
                RightLeft_Manager();
                Move();
                //�X�L���������̃X�L���{�^���̋���
                if (skillnow == true)
                {
                    SkillButtonCoolTime();
                }
                //�f�o�t�����̏���
                if (slower == true)
                {
                    EliminateSlower();
                }

                //�S�[��
                Goal();

            }
            else if (countdown < -5)   //���U���g��ʂփV�[���J��
            {
                SceneManager.LoadScene("Result");
            }
        }
    }

    //�S�[�����̏���
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

    //�X�L���{�^�������������ǂ����̔���
    public void OnClickSkillButton()
    {
        skillnow = true;
        skillSoundOn = true;
    }

    //�X�L����������Ĕ����܂ł̃N�[���^�C���ƃX�L���{�^���̋���
    void SkillButtonCoolTime()
    {
        //�X�L���{�^���̎g�p�s�ɂ���
        playerManager[0].interactable = false;
        //�X�L���̌��ʎ��Ԓ��ł��邱�Ƃ𖾋L����
        skilltext.text = "�������I";
        //�X�L���������Ԃ��J�E���g����
        skilltimer += Time.deltaTime;
        //10�b��A�X�L�����I�����A�N�[���^�C���ɓ���
        if (skilltimer > 10)
        {
            //�X�L���������ɕύX�����t���O��߂�
            speed = originalspeed;
            muteki = false;
            speedUp = false;
            secondjump = false;
            //�N�[���^�C�����J�E���g����
            cooltime -= Time.deltaTime;
            //�N�[���^�C���̓X�L���{�^����ɕ\������
            skilltext.text = cooltime.ToString("f2");
            //�N�[���^�C�����I�������X�L�����Ďg�p�ł���悤�ɂ���
            if (cooltime < 0)
            {
                //�X�L���{�^�����g�p�\�ɂ���
                playerManager[0].interactable = true;
                //�X�L���{�^���̕\�L��߂�
                skilltext.text = "SKILL";
                //�N�[���^�C���̏����Ɋւ���ϐ���������
                skillnow = false;
                skilltimer = 0.0f;
                cooltime = 10.0f;
            }
        }
    }

    //�X�L���̓��e
    void SkillMaker(int num)
    {
        if (num == 1)//10�b�ԃX�s�[�h�A�b�v
        {
            speed = originalspeed * speedUpRate;
            speedUp = true;
        }
        if (num == 2)//10�b�ԃW�����v���\�A�b�v
        {
            secondjump = true;
        }
        if (num == 3)//10�b�Ԗ��G
        {
            muteki = true;
            //�����󂯂Ă���f�o�t�����S��
            ren.color = new Color(1f, 1f, 1f, 1f);
            speed = originalspeed;
            slower = false;
            slowertimer = 0.0f;
        }
    }

    //�W�����v�ɂ���
    public void Jump()
    {
        bool canTime = jumpLimitTime > jumpTime;
        if (isGround)
        {
            //�n�ォ��W�����v�����Ƃ�
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
        //�W�����v���̐���
        else if (isJump)
        {
            //�W�����v�̍��x�����A�؋󎞊Ԃ�݂���
            if (pushjump == true && jumpPos + jumpHeight > transform.position.y && canTime)  //���x�������A�؋󎞊ԓ��ł͏㏸
            {
                ySpeed = jumpPower;
                jumpTime += Time.deltaTime;
            }
            else  //���x�����������͑؋󎞊Ԃ𒴂���Ə㏸���Ȃ�
            {
                isJump = false;
                jumpTime = 0.0f;
                ySpeed = 0.0f;
                pushjump = false;
            }
        }
        //�󒆂Ńv���C���[���㏸���Ă��Ȃ��Ƃ��A���A(2�i�W�����v�̃X�L���������܂���2�i�W�����v��������2�i�W�����v�ς�)�̂Ƃ��ɃW�����v���悤�Ƃ����Ƃ�
        else if (pushjump && !isGround && !isJump && (!secondjump || (secondjump && jumpCount == 2)))
        {
            //�W�����v�{�^���������Ă��W�����v���Ȃ�
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

    //���E�̈ړ����{�^���Ɗ֘A�t���邽�߂̒i���
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

    //���E�̈ړ��̃t���O
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

    //�ړ��ƃW�����v
    void Move()
    {
        float xSpeed_right = 0.0f;
        float xSpeed_left = 0.0f;
        Vector2 myGravity = new Vector2(0, 0);

        //�E�ֈړ�����Ƃ��̋���
        if (rightMove == true)
        {
            xSpeed_right = speed;
        }
        else if (rightMove == false)
        {
            xSpeed_right = 0.0f;
        }
        //���ֈړ�����Ƃ��̋���
        if (leftMove == true)
        {
            xSpeed_left = -speed;
        }
        else if (leftMove == false)
        {
            xSpeed_left = 0.0f;
        }
        //�����̋���
        if (isJump == false && isGround == false)
        {
            myGravity = new Vector2(0, -jumpPower);
        }
        
        //PC�ł̃o�b�N�p�F�X�y�[�X�L�[�ŃW�����v����悤�Ɋ��蓖��
        if (Input.GetKeyDown("space"))
        {
            pushjump = true;
            jumpSoundOn = true;
        }
        else if (Input.GetKeyUp("space"))
        {
            pushjump = false;
        }
        
        //�W�����v���̋���
        Jump();

        //�������ɏ�����Ƃ��̃v���C���[�̑��x
        Vector2 addVelocity = Vector2.zero;
        if (moveObj != null)
        {
            addVelocity = moveObj.GetVelocity();
        }
        rb.velocity = new Vector2(xSpeed_left + xSpeed_right, ySpeed) + myGravity + addVelocity;

        //PC�ł̃o�b�N�p�FS�L�[�ŃX�L����������悤�Ɋ��蓖��
        if (Input.GetKeyDown("s"))
        {
            skillnow = true;
            skillSoundOn = true;
            SkillMaker(skill);
        }
        SkillSound();
    }

    //�����ƂԂ������Ƃ�
    void Collision()
    {
        //���n
        if (isGround)
        {
            jumpCount = 0;
        }
        //�󒆂ɂ���Ƃ�
        else if (isGround == false && jumpCount == 0)
        {
            jumpCount = 1;
        }
        //�f�o�t
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
        //��Q���ɓ��������Ƃ�
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

        //�G�̍U���ɓ��������Ƃ�
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

        //�������ɏ�����Ƃ�
        if (col.gameObject.tag == "Movefloor")
        {
            moveObj = col.gameObject.GetComponent<MoveObject>();
        }

    }

    //�������痣�ꂽ��
    private void OnCollisionExit2D(Collision2D col)
    {
        //���������痣�ꂽ
        if (col.gameObject.tag == "Movefloor")
        {
            moveObj = null;
        }

        //Enemy���痣�ꂽ
        if (col.gameObject.tag == "Enemy")
        {
            onEnemy = false;
        }
    }

    //�f�o�t
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

    //�f�o�t�����̏���
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
