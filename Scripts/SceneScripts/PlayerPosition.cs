using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    private GameObject PlayerObj;



    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoaded�Ɋ֐���ǉ�
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //�V�[�������[�h���ꂽ��Ăяo�����

        PlayerObj = GameObject.Find("Player");
        PlayerObj.transform.position = gameObject.transform.position;

    }
}
