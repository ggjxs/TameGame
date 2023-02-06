using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour
{

    private GameManager GameM;

    private Rigidbody2D Rbody;

    [SerializeField] private string Floor = "Floor", PlayerName = "Player";

    [SerializeField] private float JunpPawer, AttackD, Speed;//�W�����v�́A�U���́A�ړ����x

    [SerializeField] private GameObject sikai;

    [Header("�ǂ�������Ώ�")] [SerializeField] private GameObject Player;

    private Vector2 RayDirection;

    private Ray2D ray;
    private sikai sikaiSlime;
    private LayerMask layerMask;

    private int EnemyHori = -1;


    private enum EnemyState
    {
        Walk,
        Wait,
        Chase
    };

    //STATE�^�̕ϐ�
    EnemyState state;

    void Start()
    {
        state = EnemyState.Walk;
        sikaiSlime = sikai.GetComponent<sikai>();
        Rbody = GetComponent<Rigidbody2D>();
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        layerMask = LayerMask.GetMask("Player&folor");
        Debug.Log(layerMask);
    }

    void Update()
    {
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Floor) 
            Rbody.AddForce(Vector2.up * JunpPawer);
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
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
        }
        else
            return;
    }
}
