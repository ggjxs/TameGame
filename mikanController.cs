using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mikanController : MonoBehaviour
{
    public float MikanAttackD { get; private set; } = 0.0f;

    private GameManager GameM;

    [SerializeField] private string Floor = "Floor", Player = "Player";

    private Rigidbody2D Rbody;

    [SerializeField] private float JunpPawer, AttackD,Speed, WalkRange;//ジャンプ力、攻撃力、探索距離

    [SerializeField] private GameObject Target;

    private float StartPos;

    public enum EnemyState
    {
        Walk,
        Wait,
        Chase
    };

    //STATE型の変数
    EnemyState state;

    private void Awake()
    {
        MikanAttackD = AttackD;
        Rbody = GetComponent<Rigidbody2D>();
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }
    void Start()
    {
        Rbody = GetComponent<Rigidbody2D>();
        StartPos = transform.position.x;
        state = EnemyState.Walk;
    }

    // Update is called once per frame
    void Update()
    {
        
        Walk();
        Wait();
        Chase();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == Floor)
            Rbody.AddForce(Vector2.up * JunpPawer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Player)
        {
            state = EnemyState.Chase;
        }        
    }
    private void Walk()
    {
        //目的地まで移動する処理

        if (state == EnemyState.Walk)
        {
           
            transform.position = new Vector3(StartPos + Mathf.PingPong(Time.time, WalkRange), transform.position.y, transform.position.z);
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
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        }
        else
            return;
    }

    

    private void OnDestroy()
    {
        
        
    }
}
