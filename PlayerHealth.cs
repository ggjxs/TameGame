using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private GameManager GameM;
    private PlayerController PlCon;

    [SerializeField] private float PlayerHp,knockBackPower;//PlayerのHp,ノックバックする力

    private Rigidbody2D Rbody;
    private string EnemyName = "Enemy";//敵キャラのtagの名前
    

    //点滅の間隔
    [SerializeField] private float flashInterval;
    //点滅させるときのループカウント
    [SerializeField] private int loopCount;
    //点滅させるためのSpriteRenderer
    private SpriteRenderer sp;


    private Slider Hpslider;

    //プレイヤーの状態用列挙型（ノーマル、ダメージ、無敵の3種類）

    private enum PlayerState
    {
        NOMAL,
        DAMAGED,
        MUTEKI
    }
    //STATE型の変数
    PlayerState state;

    IEnumerator _hit()
    {
        sp.color = Color.black;
        for (int i = 0; i < loopCount; i++)
        {

            yield return new WaitForSeconds(flashInterval);
            sp.enabled = false;
            yield return new WaitForSeconds(flashInterval);
            sp.enabled = true;
            if (i > 20)
            {
                state = PlayerState.MUTEKI;
                sp.color = Color.green;
            }
        }
        state = PlayerState.NOMAL;
        sp.color = Color.white;
    }
    private void Awake()
    {
        PlCon = GetComponent<PlayerController>();
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //SpriteRenderer格納
        sp = GetComponent<SpriteRenderer>();
        Hpslider = GameObject.Find("PlayerSlider").GetComponent<Slider>();
        Hpslider.maxValue = PlayerHp;
        Rbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
        
    }

    void Update()
    {
        Hpslider.value = PlayerHp;

        //ステートがダメージならリターン
        if (state == PlayerState.DAMAGED)
        {
            return;
        }
    }
    void FixedUpdate()
    {
        if (PlayerHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void knockBack()
    {
        var AttackHori = PlCon.AttackHori;


        if (AttackHori == 0)
            Rbody.AddForce(new Vector2(-knockBackPower, Rbody.velocity.y + 1), ForceMode2D.Impulse);
        else if (AttackHori > 0)
            Rbody.AddForce(new Vector2(-knockBackPower, Rbody.velocity.y + 1), ForceMode2D.Impulse);
        else if (AttackHori < 0)
            Rbody.AddForce(new Vector2(knockBackPower, Rbody.velocity.y + 1), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyName))    
        {
            //無敵状態なら処理を行わない
            if (state == PlayerState.MUTEKI) 
            {
                return;
            }

            if (state == PlayerState.NOMAL)
            {
                knockBack();
                PlayerHp -=GameM.MikanAttackD;
                state = PlayerState.DAMAGED;
                //コルーチンを開始
                StartCoroutine(_hit());

            }


        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyName))
        {
            //無敵状態なら処理を行わない
            if (state == PlayerState.MUTEKI)
            {
                return;
            }

            if (state == PlayerState.NOMAL)
            {
                PlayerHp -= GameM.MikanAttackD;
                state = PlayerState.DAMAGED;
                //コルーチンを開始
                StartCoroutine(_hit());

            }
        }
    }
}
