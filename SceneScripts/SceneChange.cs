using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField]private int SceneNumber;
    [SerializeField] private float Interval, FadeOutInterval;//�V�[�����ς��܂ł̎���,�t�F�[�h�A�E�g���鑬��
    [SerializeField] private GameObject Panelfade;//�t�F�[�h�p�l���̎擾
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
        // �񓯊��Ń��[�h���s��
        var asyncLoad = SceneManager.LoadSceneAsync(SceneNumber);

        // ���[�h���������Ă��Ă��C�V�[���̃A�N�e�B�u���͋����Ȃ�
        asyncLoad.allowSceneActivation = false;

        // �t�F�[�h�A�E�g���̉�������̏���
        yield return FadeOut();

        // ���[�h�����������Ƃ��ɃV�[���̃A�N�e�B�u����������
        asyncLoad.allowSceneActivation = true;

        // ���[�h����������܂ő҂�
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
        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoaded�Ɋ֐���ǉ�
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
