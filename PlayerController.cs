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

    [SerializeField] private string FloorName = "Floor";//床のtagの名前

    private Rigidbody2D rd;
    private float Hori; //Horizontal

    [SerializeField] private float speed,AtReach;//移動速度、攻撃のリーチ

    [SerializeField] private float NextExp, LevelUpAttackD;//次のレベルアップまでの経験値、レベルアップした時に上がる攻撃力

    [SerializeField] private float AttackTime;//攻撃できるようになるまでの時間

    private float AttackTimer;//攻撃できるようになるまでの時間の計算用変数

    [SerializeReference]private GameObject AttackgameObject;//攻撃判定のgameObject

     //junp
    private float JunpTimer;  //ジャンプの現在の継続時間
    private float JunpAfterTimer;
    private bool JunpTrigger;
    private bool JunpAfterTrigger;
    [SerializeField] private float JunpTime;      //ジャンプの継続時間
    [SerializeField] private float JunpAfterTime;
    [SerializeField] private float JunpPawer;     //ジャンプの速さ
    [SerializeField] private float JunpLastPawer;

    [SerializeField]private float AttackD;//playerの攻撃力

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
        transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);//右向きにする
        
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

        if (GameM.PlayerExp >= GameM.PlayerNextExp) LevelUp();//レベルアップ
    }

    private void FixedUpdate()
    {
        PlayerMove();
        Freeze();

        if (Hori != 0)
            AttackHori = Hori;//攻撃する方向の取得

        //PositionGet = rd.position;

        if (JunpTrigger && JunpAfterTrigger == false && JunpTimer <= JunpTime) PlayerJunp();

        AttackTimer += Time.deltaTime;//攻撃のクールダウンの計算

        
    }
    
    private void PlayerMove()//プレイヤーの移動
    {
        if (Hori != 0)
        {
            //移動入力があるとき

            rd.velocity = new Vector2(speed * Hori, rd.velocity.y);
            if (Hori > 0)
                transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f); //向きを変更する
            else if (Hori < 0)
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); //向きを変更する  

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

    private void Attack()//プレイヤーの攻撃
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
    
    void PlayerJunp() //プレイヤーのジャンプ
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
        PlayerNextExp = Mathf.CeilToInt(GameM.PlayerNextExp * 1.5f);//切り上げてから格納
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
