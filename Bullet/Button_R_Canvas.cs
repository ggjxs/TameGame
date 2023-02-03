using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Button_R_Canvas : MonoBehaviour
{
    public string Bullet1 { get; set; }
    public string Bullet2 { get; set; }
    public int ClickCount { get; private set; }
    

    private bool RTrigger_Mein, ShuffingOK;
    private GameObject tmp;
    private Button_R_Player PlayerScripts;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject[] BulletList;
    
    private Vector2[] PositionList;
    public GameObject[] BulletRandom { get; private set; }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoadedに関数を追加
        PlayerScripts = Player.GetComponent<Button_R_Player>();
        RTrigger_Mein = PlayerScripts.CanvasTrigger;
        BulletRandom = new GameObject[BulletList.Length];
        Array.Copy(BulletList, BulletRandom, BulletList.Length);
        PositionList = new Vector2[BulletList.Length];
        //Debug.Log(BulletList[0].transform.position);
        for (int i = 0; i < BulletList.Length; ++i)
        {
            PositionList[i] = BulletList[i].transform.position; //ワールドポイントのバレットの座標の全取得
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerScripts = Player.GetComponent<Button_R_Player>();
        RTrigger_Mein = PlayerScripts.CanvasTrigger;
        BulletRandom = new GameObject[BulletList.Length];
        Array.Copy(BulletList, BulletRandom, BulletList.Length);
        PositionList = new Vector2[BulletList.Length];
        //Debug.Log(BulletList[0].transform.position);
        for (int i = 0; i < BulletList.Length; ++i)
        {
            PositionList[i] = BulletList[i].transform.position; //ワールドポイントのバレットの座標の全取得
        }
    }

    void Update()
    {
        RTrigger_Mein = PlayerScripts.CanvasTrigger;

        if (RTrigger_Mein)
        {
            if (PlayerScripts.RandomCount == 0)
            {
                PlayerScripts.RandomCount++;

                RandomShuffing();
                if (ShuffingOK)
                {
                    BulletHyouzi();
                }
            }

            if (ClickCount == 2)
            {
                SwapBullet();
            }

        }
    }

    private void FixedUpdate()
    {
        
    }


    private void RandomShuffing()　　//バレットの初期配置のシャッフル
    {
        System.Random rng = new System.Random();
        for (int n = BulletRandom.Length - 1; n >= 0; n--)
        {
            int k = rng.Next(n);

            tmp = BulletRandom[k];
            BulletRandom[k] = BulletRandom[n];
            BulletRandom[n] = tmp;
        }
        ShuffingOK = true;

    }

    private void BulletHyouzi()　//バレットの初期配置へのポジション変更
    {
        for (int p = 0; p < BulletRandom.Length; ++p)
        {
            BulletRandom[p].transform.position = PositionList[p];
        }
    }

    private void SwapBullet()
    {
        ShuffingOK = false;
        var Bullet1No = namaetannsaku(Bullet1);
        var Bullet2No = namaetannsaku(Bullet2);

        BulletRandom[Bullet1No].transform.position = PositionList[Bullet2No];//ポジション変更
        BulletRandom[Bullet2No].transform.position = PositionList[Bullet1No];

        var Clicktmp = BulletRandom[Bullet1No];//配列の入れ替え
        BulletRandom[Bullet1No] = BulletRandom[Bullet2No];
        BulletRandom[Bullet2No] = Clicktmp;

        Bullet1 = null;//初期化
        Bullet2 = null;
        ClickCount = 0;


    }

    private int namaetannsaku(string Bulletname) //SwapBullet内で使用
    {
        for (int t = 0; t < BulletRandom.Length; t++)
        {
            if (BulletRandom[t].name == Bulletname)
            {
                return t;
            }
        }
        Debug.Log("エラー");
        return -1;
    }

    private void ClickCountUp()
    {
        ClickCount++;
    }

    private void ClickCountZero()
    {
        ClickCount = 0;
    }

}

