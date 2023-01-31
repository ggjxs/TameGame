using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    private GameManager GameM;

    private Rigidbody2D Rbody;

    [SerializeField] private string Floor = "Floor", PlayerName = "Player";

    [SerializeField] private float JunpPawer, AttackD, Speed;//ジャンプ力、攻撃力、移動速度

    [SerializeField] private GameObject sikai;

    [Header("追いかける対象")] [SerializeField] private GameObject Player;

    [Header("画面外でも行動する")] [SerializeField] private bool NonVisibleAct;

    private SpriteRenderer Spr;

    private Vector2 RayDirection;

    private Ray2D ray;
    private sikai sikaiSlime;
    private LayerMask layerMask;


    private bool LeftFlag = false;

    private enum EnemyState
    {
        Walk,
        Wait,
        Chase
    };

    //STATE型の変数
    EnemyState state;

    private void Awake()
    {
        sikaiSlime = sikai.GetComponent<sikai>();
        Rbody = GetComponent<Rigidbody2D>();
        Spr = GetComponent<SpriteRenderer>();
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    void Start()
    {
        state = EnemyState.Walk;
        


        layerMask = LayerMask.GetMask("Player&folor");
        Debug.Log(layerMask);

       
    }

    // Update is called once per frame
    void Update()
    {
        if (Spr.isVisible || NonVisibleAct)
        {
            //行動する

            Walk();
            Wait();
            Chase();

            if (sikaiSlime.ColliderTrigger == true)
            {
                RayDirection = new Vector2(Player.transform.position.x - Rbody.position.x, Player.transform.position.y - Rbody.position.y);
                RaycastHit2D hit = Physics2D.Raycast(Rbody.transform.position, RayDirection, 10.0f, layerMask);
                Debug.DrawRay(Rbody.position, RayDirection, Color.red);

                if (hit)
                    state = EnemyState.Chase;
            }
        }
  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == Floor)
            Rbody.AddForce(Vector2.up * JunpPawer);
    }
    
    private void Walk()
    {
        //目的地まで移動する処理

        if (state == EnemyState.Walk)
        {
            int EnemyHori = -1;
            if (LeftFlag)
            {
                EnemyHori = 1;
                transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f); //向きを変更する
            }
            else
            {
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); //向きを変更する
            }

            Rbody.velocity = new Vector2(EnemyHori * Speed, Rbody.velocity.y);
        }
        else
            return;
    }
    private void Wait()
    {
        //目的地で待つ処理

        if (state == EnemyState.Wait)
        {
            
        }
        else
            return;
    }
    private void Chase()
    {
        //追いかける処理

        if (state == EnemyState.Chase)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
        }
        else
            return;
    }
}
