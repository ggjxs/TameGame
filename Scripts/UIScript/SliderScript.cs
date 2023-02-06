using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SliderScript : MonoBehaviour
{
    //ターゲットするカメラ
    [SerializeField] private Camera TargetCamera;
    //UIを表示するオブジェクトと表示するUI
    [SerializeField] private Transform Target, TargetUI;
    //オブジェクトの位置のオフセット
    [SerializeField] private Vector3 WorldOffset;

    private RectTransform ParentUI;

    //初期化メソッド
    public void Initialize(Transform target, Camera targetCamera = null)
    {
        Target = target;
        TargetCamera = targetCamera != null ? targetCamera : Camera.main;

        OnUpdatePosition();
    }
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoadedに関数を追加

        // カメラが指定されていなければメインカメラにする
        if (TargetCamera == null)
            TargetCamera = Camera.main;
        if (TargetUI == null)
            TargetUI = gameObject.transform;

        // 親UIのRectTransformを保持
        ParentUI = TargetUI.parent.GetComponent<RectTransform>();

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // カメラが指定されていなければメインカメラにする
        if (TargetCamera == null)
            TargetCamera = Camera.main;
        if (TargetUI == null)
            TargetUI = gameObject.transform;

        // 親UIのRectTransformを保持
        ParentUI = TargetUI.parent.GetComponent<RectTransform>();
    }
    // UIの位置を毎フレーム更新
    private void Update()
    {
        Des();
        OnUpdatePosition();
    }
    // UIの位置を更新する
    private void OnUpdatePosition()
    {
        var cameraTransform = TargetCamera.transform;

        // カメラの向きベクトル
        var cameraDir = cameraTransform.forward;
        // オブジェクトの位置
        var targetWorldPos = Target.position + WorldOffset;
        // カメラからターゲットへのベクトル
        var targetDir = targetWorldPos - cameraTransform.position;

        // 内積を使ってカメラ前方かどうかを判定
        //3Dゲームの場合必要
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // カメラ前方ならUI表示、後方なら非表示
        TargetUI.gameObject.SetActive(isFront);
        if (!isFront) return;

        // オブジェクトのワールド座標→スクリーン座標変換
        var targetScreenPos = TargetCamera.WorldToScreenPoint(targetWorldPos);

        // スクリーン座標変換→UIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ParentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        //第１引数には、変換先のRectTransformローカル座標の親を指定します。

        //第２引数には、変換元のスクリーン座標を指定します。

        //第３引数には、Canvasに関連するカメラを指定します。Canvasがオーバーレイモード[1] 場合はnullを指定しなければいけません。

        //第４引数には、RectTransformのローカル座標を受け取るための変数を指定します。
        );

        // RectTransformのローカル座標を更新
        TargetUI.localPosition = uiLocalPos;
    }
    private void Des()
    {
        if (Target == null)
        {
            Destroy(gameObject);
        }
    }
    
}
