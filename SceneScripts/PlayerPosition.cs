using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    private GameObject PlayerObj;



    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoadedに関数を追加
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
        //シーンがロードされたら呼び出される

        PlayerObj = GameObject.Find("Player");
        PlayerObj.transform.position = gameObject.transform.position;

    }
}
