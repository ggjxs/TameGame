using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Vector3 PositionGet { get; private set; }
    public float HoriGet { get; private set; } = 1;
    public int PlayerLevel { get; private set; } = 1;
    public float PlayerAtD { get; private set; }

    public float PlayerExp { get; private set; }
    public float PlayerNextExp { get; private set; }
    public float AttackHori { get; private set; }//�U������

    private Button_R_Player ButtonPlayer;

    private GameManager GameM;

    [Header ("����tag�̖��O")][SerializeField] private string FloorName = "Floor";//����tag�̖��O

    private Rigidbody2D rd;
    private float Hori; //Horizontal

    private bool MoveButton = false;

    [Header("�_�b�V���̎�t����")] [SerializeField] private float MaxTapTimer;
    private float TapTimer; //�^�b�v�J�n����̗ݐώ���
    private int TapCount;

    [Header("�ړ�")] [SerializeField] private float Speed ;//�ړ����x

    [SerializeField] private float Runspeed;//�_�b�V���X�s�[�h


    [Header("�o���l")] [SerializeField] private float NextExp;//���̃��x���A�b�v�܂ł̌o���l



    [Header("�U��")] [SerializeField] private float AttackD;//player�̍U����

    [SerializeField] private float AtReach;//�U���̃��[�`

    [SerializeField] private float LevelUpAttackD;//���x���A�b�v�������ɏオ��U����

    [SerializeField] private float AttackTime;//�U���ł���悤�ɂȂ�܂ł̎���

    private float AttackTimer;//�U���ł���悤�ɂȂ�܂ł̎��Ԃ̌v�Z�p�ϐ�

    [SerializeReference] private GameObject AttackgameObject;//�U�������gameObject



    //�W�����v�֘A
    private float JunpTimer;  //�W�����v�̌��݂̌p������
    private float JunpAfterTimer;
    private bool JunpAfterTrigger;
    [Header("�W�����v")] [SerializeField] private float JunpTime;//�W�����v�̌p������
    [SerializeField] private float JunpAfterTime;
    [SerializeField] private float JunpPawer;     //�W�����v�̑���
    [SerializeField] private float JunpLastPawer;

    [Header("���̑�")] [SerializeField] private PhysicsMaterial2D PM2d;

    private Animator anim = null;

    private bool isGround;

    [SerializeField] private GameObject hed;

    [SerializeField] private GameObject leg;

    private GroundCheckScript hedScript;

    private GroundCheckScript legScript;

    private void Awake()
    {
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        ButtonPlayer = GetComponent<Button_R_Player>();
        anim = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        PlayerAtD = AttackD;
        PlayerNextExp = NextExp;
    }

    void Start()
    {
        PositionGet = rd.position;
        transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);//�E�����ɂ���
        hedScript = hed.GetComponent<GroundCheckScript>();
        legScript = leg.GetComponent<GroundCheckScript>();

    }

    void Update()
    {
        if (isGround)
        {
            if (PM2d.friction != 0.5f)
            {
                //�n�ʂɂ��Ă����疀�C���N����悤�ɂ���
                PM2d.friction = 0.5f;
            }
        }
        

        isGround = legScript.isground;

        if (ButtonPlayer.TimeStop == false)
        {
            //���Ԃ��~�܂��Ă��邩�̊m�F


            Hori = Input.GetAxisRaw("Horizontal");

            if (Hori != 0 && Hori != HoriGet) HoriGet = Hori;

            if (Input.GetMouseButtonDown(0))
                Attack();

            if (MoveButton)
                TapTimer += Time.deltaTime;


            if (Input.GetAxisRaw("Jump") == 1) anim.SetBool("jump", true);

            if (Input.GetKey(KeyCode.Space) == false && isGround == false) JunpAfterTrigger = true;

            if (GameM.PlayerExp >= GameM.PlayerNextExp) LevelUp();//���x���A�b�v

            if (TapTimer < MaxTapTimer)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    MoveButton = true;
                    TapCount++;
                    Debug.Log(TapCount);
                }
            }
            else if (Hori == 0)
            {
                TapCount = 0;
                TapTimer = 0.0f;
            }

            //LegTriggerStay();
            LegTriggerEnter();

        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
        Freeze();

        if (Hori != 0)
            AttackHori = Hori;//�U����������̎擾

        //PositionGet = rd.position;

        if (isGround && Input.GetKey(KeyCode.Space) && JunpAfterTrigger == false && JunpTimer <= JunpTime)
        {
            PlayerJunp();
            PM2d.friction = 0.0f;
        }
        AttackTimer += Time.deltaTime;//�U���̃N�[���_�E���̌v�Z


    }

    private void PlayerMove()//�v���C���[�̈ړ�
    {
        if (Hori != 0)
        {
            //�ړ����͂�����Ƃ�

            if (TapCount <= 1)
                rd.velocity = new Vector2(Speed * Hori, rd.velocity.y);
            else if (TapCount >= 2)
                rd.velocity = new Vector2(Runspeed * Hori, rd.velocity.y);


            if (Hori > 0)
                transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f); //������ύX����
            else if (Hori < 0)
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); //������ύX����  

        }
        else if (TapCount >= 2)
            TapCount = 0;
        else
            anim.SetBool("walk", false);

        if (isGround)
        {
            
            if (Input.GetKey(KeyCode.Space)) anim.SetBool("jump", true);


            if (Hori != 0)
            {
                anim.SetBool("walk", true);
                anim.SetBool("wind", false);
            }
            else
            {
                anim.SetBool("wind", true);
                anim.SetBool("walk", false);
            }
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("wind", false);
        }
    }


    private void Attack()//�v���C���[�̍U��
    {
        if (AttackTimer > AttackTime)
        {
            if (Hori == 0)
            {
                if (AttackHori == 0)
                {
                    Instantiate(AttackgameObject, new Vector3(transform.position.x + AtReach, transform.position.y, transform.position.z), new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));

                }
            }
            if (AttackHori > 0)
            {
                Instantiate(AttackgameObject, new Vector3(transform.position.x + AtReach, transform.position.y, transform.position.z), new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));

            }
            else if (AttackHori < 0)
            {
                Instantiate(AttackgameObject, new Vector3(transform.position.x - AtReach, transform.position.y, transform.position.z), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            }
            AttackTimer = 0;
        }

    }

    void Freeze()
    {
        if (isGround == false)
        {
            anim.SetBool("frieze", true);
        }
    }

    void PlayerJunp() //�v���C���[�̃W�����v
    {

        JunpTimer += Time.deltaTime;
        rd.velocity = new Vector3(rd.velocity.x, JunpPawer, 0);
        if (JunpAfterTrigger && JunpAfterTimer <= JunpAfterTime)
        {
            JunpAfterTimer += Time.deltaTime;
            new Vector3(rd.velocity.x, JunpLastPawer, 0);
        }

    }
    private void LevelUp()
    {
        PlayerExp -= GameM.PlayerNextExp;
        PlayerNextExp = Mathf.CeilToInt(GameM.PlayerNextExp * 1.5f);//�؂�グ�Ă���i�[
        PlayerAtD += LevelUpAttackD;
        PlayerLevel++;
    }

    private void LegTriggerEnter()
    {
        if (isGround)
        {
            if (JunpAfterTrigger) JunpAfterTrigger = false;
            if (JunpTimer != 0) JunpTimer = 0;
            if (JunpAfterTimer != 0) JunpAfterTimer = 0;
            anim.SetBool("jump", false);
            anim.SetBool("frieze", false);
        }
    }



    private void LegTriggerStay()
    {
        if (isGround)
        {
            if (JunpAfterTrigger) JunpAfterTrigger = false;
            if (JunpTimer != 0) JunpTimer = 0;
            if (JunpAfterTimer != 0) JunpAfterTimer = 0;
            anim.SetBool("jump", false);
            anim.SetBool("frieze", false);
        }
    }

}