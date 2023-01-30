using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    [SerializeField] private GameObject Prefab;

    [SerializeField] private GameObject UIPrefab;
    
    [SerializeField]
    [Tooltip("Spawner1")]
    private Transform rangeA;
    [SerializeField]
    [Tooltip("Spawner2")]
    private Transform rangeB;
    [SerializeField] private float IntervalTimer;

    private Transform Canvas;

    // 経過時間,
    private float time;

    private void Start()
    {
        Canvas = GameObject.Find("Canvas").transform;

        //// rangeAとrangeBのx座標の範囲内でランダムな数値を作成
        //float x = Random.Range(rangeA.position.x, rangeB.position.x);
        //// rangeAとrangeBのy座標の範囲内でランダムな数値を作成
        //float y = Random.Range(rangeA.position.y, rangeB.position.y);
        //// rangeAとrangeBのz座標の範囲内でランダムな数値を作成
        //float z = Random.Range(rangeA.position.z, rangeB.position.z);

        ////GameObject Slider = Instantiate(UIPrefab, Vector3.zero, Quaternion.identity);
        ////Slider.transform.SetParent(Canvas);

        //// GameObjectを上記で決まったランダムな場所に生成
        //GameObject Mikan = Instantiate(Prefab, new Vector3(x, y, z), Quaternion.identity);

       
    }

    void Update()
    {
        // 前フレームからの時間を加算していく
        time += Time.deltaTime;

        // 約1秒置きにランダムに生成されるようにする。
        if (time > IntervalTimer)
        {
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
            float y = Random.Range(rangeA.position.y, rangeB.position.y);
            // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            // GameObjectを上記で決まったランダムな場所に生成
            var Mikan = Instantiate(Prefab, new Vector3(x, y, z), Prefab.transform.rotation);
           
            GameObject Slider = Instantiate(UIPrefab, Vector3.zero, Prefab.transform.rotation);

            Slider.transform.SetParent(Canvas);

            // 経過時間リセット
            time = 0f;
        }
    }
}
