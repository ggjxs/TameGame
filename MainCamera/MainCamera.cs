using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoadedに関数を追加
    }
    void Start()
    {
        Player = GameObject.Find("Player");
    }


    void FixedUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 3f, -10f);       
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Player = GameObject.Find("Player");
    }
}
