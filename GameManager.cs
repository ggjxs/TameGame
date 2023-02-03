using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private PlayerController PlayerCon;

    //�v���C���[�̍U����
    public float PlyerAttckD { get; private set; } = 0.0f;

    //�v���C���[�̏����o���l
    public float PlayerExp { get; private set; } = 0.0f;

    //�v���C���[�̃��x��
    public int PlayerLevel { get; private set; } = 1;

    //�v���C���[�����Ƃǂꂭ�炢�Ń��x���A�b�v���邩
    public float PlayerNextExp { get; private set; } = 0.0f;

    public static GameManager instance;

    private Text ExpText, NextExpText, LevelText;

    private void Awake()
    {
        //�V�[�����ړ����Ă��I�u�W�F�N�g�������Ȃ��悤�ɂ���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoaded�Ɋ֐���ǉ�

        //PlayerCon = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        //PlyerAttckD = PlayerCon.PlayerAtD;
        //ExpText = GameObject.Find("ExpText").GetComponent<Text>();
        //NextExpText = GameObject.Find("NextExpText").GetComponent<Text>();
        //LevelText = GameObject.Find("LevelText").GetComponent<Text>();



    }

    void Update()
    {
        PlayerExp = PlayerCon.PlayerExp;
        PlayerNextExp = PlayerCon.PlayerNextExp;

        ExpText.text = string.Format("Exp:{0}", PlayerExp);
        NextExpText.text = string.Format("NextLevel:{0}", PlayerNextExp);
        LevelText.text = string.Format("Level:{0}", PlayerLevel);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //�V�[�������[�h���ꂽ��Ăяo�����
        ExpText = GameObject.Find("ExpText").GetComponent<Text>();
        NextExpText = GameObject.Find("NextExpText").GetComponent<Text>();
        LevelText = GameObject.Find("LevelText").GetComponent<Text>();
        PlayerCon = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log("�V�[�������[�h����܂���");

    }
}
