using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField]private int SceneNumber;
    [SerializeField] private float Interval, FadeOutInterval;//シーンが変わるまでの時間,フェードアウトする速さ
    [SerializeField] private GameObject Panelfade;//フェードパネルの取得
    [SerializeField] private string PlayerName = "Player";
    [SerializeField] private int loopCount = 100;
    private Image PanelImage;
    private float Alpha;
    private string Scenecurrent;
    private bool isLoad;

    private IEnumerator SceneStart()
    {
        
        Panelfade.SetActive(true);
        PanelImage = Panelfade.GetComponent<Image>();
        // 非同期でロードを行う
        var asyncLoad = SceneManager.LoadSceneAsync(SceneNumber);

        // ロードが完了していても，シーンのアクティブ化は許可しない
        asyncLoad.allowSceneActivation = false;

        // フェードアウト等の何かしらの処理
        yield return FadeOut();

        // ロードが完了したときにシーンのアクティブ化を許可する
        asyncLoad.allowSceneActivation = true;

        // ロードが完了するまで待つ
        yield return asyncLoad;

    }

    public IEnumerator FadeOut()
    {
        for( int i = 0; i < loopCount; i++)
        {
            Alpha += 0.01f;
            yield return new WaitForSeconds(FadeOutInterval);
            PanelImage.color = new Color(0, 0, 0, Alpha);         
        }
        yield return new WaitForSeconds(Interval);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoadedに関数を追加
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Scenecurrent = SceneManager.GetActiveScene().name;
        isLoad = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(Scenecurrent);

        if (isLoad)
        {
            StartCoroutine(SceneStart());
            isLoad = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerName)
        {
            isLoad = true;
        }      
    }
    
}
