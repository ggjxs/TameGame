using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mikanController : MonoBehaviour
{
    public float MikanAttackD { get; private set; } = 0.0f;

    private GameManager GameM;

    private Rigidbody2D Rbody;

    [SerializeField] private string Floor = "Floor", Player = "Player";

    [SerializeField] private float JunpPawer, AttackD, Speed, WalkRange;//�W�����v�́A�U���́A�T������

    [SerializeField] private GameObject Target;

    private int EnemyHori = -1;
    private float StartPos;
   

    private enum EnemyState
    {
        Walk,
        Wait,
        Chase
    };

    //STATE�^�̕ϐ�
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Player)
        {
            state = EnemyState.Wait;
        }
    }
    private void Walk()
    {
        //�ړI�n�܂ňړ����鏈��

        if (state == EnemyState.Walk)
        {

            if (EnemyHori == -1)
            {
                transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); //������ύX����
            }
            else if (EnemyHori == 1)
            {
                transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f); //������ύX����
            }

            Rbody.velocity = new Vector2(EnemyHori * Speed, Rbody.velocity.y);
        }
        else
            return;
    }
    private void Wait()
    {
        //�ړI�n�ő҂���

        if (state == EnemyState.Wait)
        {
            if (EnemyHori == -1)
                EnemyHori = 1;
            else if (EnemyHori == 1)
                EnemyHori = -1;
            state = EnemyState.Walk;
        }
        else
            return;
    }
    private void Chase()
    {
        //�ǂ������鏈��

        if (state == EnemyState.Chase)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        }
        else
            return;
    }

}
