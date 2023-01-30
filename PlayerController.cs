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
    public float AttackHori { get; private set; }



    public static PlayerController instance;

    private GameManager GameM;

    [SerializeField] private string FloorName = "Floor";//����tag�̖��O

    private Rigidbody2D rd;
    private float Hori; //Horizontal

    [SerializeField] private float speed,AtReach;//�ړ����x�A�U���̃��[�`

    [SerializeField] private float NextExp, LevelUpAttackD;//���̃��x���A�b�v�܂ł̌o���l�A���x���A�b�v�������ɏオ��U����

    [SerializeField] private float AttackTime;//�U���ł���悤�ɂȂ�܂ł̎���

    private float AttackTimer;//�U���ł���悤�ɂȂ�܂ł̎��Ԃ̌v�Z�p�ϐ�

    [SerializeReference]private GameObject AttackgameObject;//�U�������gameObject

     //junp
    private float JunpTimer;  //�W�����v�̌��݂̌p������
    private float JunpAfterTimer;
    private bool JunpTrigger;
    private bool JunpAfterTrigger;
    [SerializeField] private float JunpTime;      //�W�����v�̌p������
    [SerializeField] private float JunpAfterTime;
    [SerializeField] private float JunpPawer;     //�W�����v�̑���
    [SerializeField] private float JunpLastPawer;

    [SerializeField]private float AttackD;//player�̍U����

    private Animator anim = null;

    private bool isGround;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        PlayerAtD = AttackD;
        PlayerNextExp = NextExp;
    }

    void Start()
    {      
        PositionGet = rd.position;
        transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);//�E�����ɂ���
        
    }

    void Update()
    {
        

        Hori = Input.GetAxisRaw("Horizontal");

        if (Hori != 0 && Hori != HoriGet) HoriGet = Hori;

        if (Input.GetMouseButtonDown(0))
            Attack();


        if (Input.GetAxisRaw("Jump") == 1) anim.SetBool("jump", true);

        if (Input.GetKey(KeyCode.Space) == false && JunpTrigger == true) JunpAfterTrigger = true;
        if (Input.GetKey(KeyCode.Space)) JunpTrigger = true;

        if (GameM.PlayerExp >= GameM.PlayerNextExp) LevelUp();//���x���A�b�v
    }

    private void FixedUpdate()
    {
        PlayerMove();
        Freeze();

        if (Hori != 0)
            AttackHori = Hori;//�U����������̎擾

        //PositionGet = rd.position;

        if (JunpTrigger && JunpAfterTrigger == false && JunpTimer <= JunpTime) PlayerJunp();

        AttackTimer += Time.deltaTime;//�U���̃N�[���_�E���̌v�Z

        
    }
    
    private void PlayerMove()//�v���C���[�̈ړ�
    {
        if (Hori != 0)
        {
            //�ړ����͂�����Ƃ�

            rd.velocity = new Vector2(speed * Hori, rd.velocity.y);
            if (Hori > 0)
                transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f); //������ύX����
            else if (Hori < 0)
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); //������ύX����  

            if (isGround)
            {
                anim.SetBool("walk", true);
                anim.SetBool("jump", true);
                anim.SetBool("wind", true);
            }
        }
        else
            anim.SetBool("walk", false);           
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == FloorName)
            isGround = true;

        if (JunpTrigger) JunpTrigger = false;
        if (JunpAfterTrigger) JunpAfterTrigger = false;
        if (JunpTimer != 0) JunpTimer = 0;
        if (JunpAfterTimer != 0) JunpAfterTimer = 0;
        anim.SetBool("jump", false);
        anim.SetBool("frieze", false);
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == FloorName)
            isGround = true;

        if (JunpTrigger) JunpTrigger = false;
        if (JunpAfterTrigger) JunpAfterTrigger = false;
        if (JunpTimer != 0) JunpTimer = 0;
        if (JunpAfterTimer != 0) JunpAfterTimer = 0;
        anim.SetBool("jump", false);
        anim.SetBool("frieze", false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == FloorName)
            isGround = false;
    }
}
