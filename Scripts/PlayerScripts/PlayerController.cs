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
    public float AttackHori { get; private set; }//攻撃方向

    private Button_R_Player ButtonPlayer;

    private GameManager GameM;

    [Header ("床のtagの名前")][SerializeField] private string FloorName = "Floor";//床のtagの名前

    private Rigidbody2D rd;
    private float Hori; //Horizontal

    private bool MoveButton = false;

    [Header("ダッシュの受付時間")] [SerializeField] private float MaxTapTimer;
    private float TapTimer; //タップ開始からの累積時間
    private int TapCount;

    [Header("移動")] [SerializeField] private float Speed ;//移動速度

    [SerializeField] private float Runspeed;//ダッシュスピード


    [Header("経験値")] [SerializeField] private float NextExp;//次のレベルアップまでの経験値



    [Header("攻撃")] [SerializeField] private float AttackD;//playerの攻撃力

    [SerializeField] private float AtReach;//攻撃のリーチ

    [SerializeField] private float LevelUpAttackD;//レベルアップした時に上がる攻撃力

    [SerializeField] private float AttackTime;//攻撃できるようになるまでの時間

    private float AttackTimer;//攻撃できるようになるまでの時間の計算用変数

    [SerializeReference] private GameObject AttackgameObject;//攻撃判定のgameObject



    //ジャンプ関連
    private float JunpTimer;  //ジャンプの現在の継続時間
    private float JunpAfterTimer;
    private bool JunpAfterTrigger;
    [Header("ジャンプ")] [SerializeField] private float JunpTime;//ジャンプの継続時間
    [SerializeField] private float JunpAfterTime;
    [SerializeField] private float JunpPawer;     //ジャンプの速さ
    [SerializeField] private float JunpLastPawer;

    [Header("その他")] [SerializeField] private PhysicsMaterial2D PM2d;

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
        transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);//右向きにする
        hedScript = hed.GetComponent<GroundCheckScript>();
        legScript = leg.GetComponent<GroundCheckScript>();

    }

    void Update()
    {
        if (isGround)
        {
            if (PM2d.friction != 0.5f)
            {
                //地面についていたら摩擦が起こるようにする
                PM2d.friction = 0.5f;
            }
        }
        

        isGround = legScript.isground;

        if (ButtonPlayer.TimeStop == false)
        {
            //時間が止まっているかの確認


            Hori = Input.GetAxisRaw("Horizontal");

            if (Hori != 0 && Hori != HoriGet) HoriGet = Hori;

            if (Input.GetMouseButtonDown(0))
                Attack();

            if (MoveButton)
                TapTimer += Time.deltaTime;


            if (Input.GetAxisRaw("Jump") == 1) anim.SetBool("jump", true);

            if (Input.GetKey(KeyCode.Space) == false && isGround == false) JunpAfterTrigger = true;

            if (GameM.PlayerExp >= GameM.PlayerNextExp) LevelUp();//レベルアップ

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
            AttackHori = Hori;//攻撃する方向の取得

        //PositionGet = rd.position;

        if (isGround && Input.GetKey(KeyCode.Space) && JunpAfterTrigger == false && JunpTimer <= JunpTime)
        {
            PlayerJunp();
            PM2d.friction = 0.0f;
        }
        AttackTimer += Time.deltaTime;//攻撃のクールダウンの計算


    }

    private void PlayerMove()//プレイヤーの移動
    {
        if (Hori != 0)
        {
            //移動入力があるとき

            if (TapCount <= 1)
                rd.velocity = new Vector2(Speed * Hori, rd.velocity.y);
            else if (TapCount >= 2)
                rd.velocity = new Vector2(Runspeed * Hori, rd.velocity.y);


            if (Hori > 0)
                transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f); //向きを変更する
            else if (Hori < 0)
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); //向きを変更する  

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