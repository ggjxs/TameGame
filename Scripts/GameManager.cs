using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private PlayerController PlayerCon;
    private PlayerHealth PlayerH;

    //プレイヤーの攻撃力
    public float PlayerAttckD { get; private set; } = 0.0f;

    //プレイヤーのHP
    public float PlayerHp { get; private set; } = 0.0f;

    //プレイヤーの所持経験値
    public float PlayerExp { get; private set; } = 0.0f;

    //プレイヤーのレベル
    public int PlayerLevel { get; private set; } = 1;

    //プレイヤーがあとどれくらいでレベルアップするか
    public float PlayerNextExp { get; private set; } = 0.0f;

    public static GameManager instance;

    private Text ExpText, NextExpText, LevelText;

    private void Awake()
    {
        //シーンを移動してもオブジェクトが消えないようにする
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //PlayerCon = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        PlayerCon = GameObject.Find("Player").GetComponent<PlayerController>();
        PlayerAttckD = PlayerCon.PlayerAtD;
        PlayerH = GameObject.Find("Player").GetComponent<PlayerHealth>();
        PlayerHp = PlayerH.PlayerHp; 
        ExpText = GameObject.Find("ExpText").GetComponent<Text>();
        NextExpText = GameObject.Find("NextExpText").GetComponent<Text>();
        LevelText = GameObject.Find("LevelText").GetComponent<Text>();     
        Debug.Log("シーンがロードされました");
    }

    void Update()
    {
        PlayerExp = PlayerCon.PlayerExp;
        PlayerNextExp = PlayerCon.PlayerNextExp;
        ExpText.text = string.Format("Exp:{0}", PlayerExp);
        NextExpText.text = string.Format("NextLevel:{0}", PlayerNextExp);
        LevelText.text = string.Format("Level:{0}", PlayerLevel);
    }
}
