using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SliderScript : MonoBehaviour
{
    //�^�[�Q�b�g����J����
    [SerializeField] private Camera TargetCamera;
    //UI��\������I�u�W�F�N�g�ƕ\������UI
    [SerializeField] private Transform Target, TargetUI;
    //�I�u�W�F�N�g�̈ʒu�̃I�t�Z�b�g
    [SerializeField] private Vector3 WorldOffset;

    private RectTransform ParentUI;

    //���������\�b�h
    public void Initialize(Transform target, Camera targetCamera = null)
    {
        Target = target;
        TargetCamera = targetCamera != null ? targetCamera : Camera.main;

        OnUpdatePosition();
    }
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;//sceneLoaded�Ɋ֐���ǉ�

        // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
        if (TargetCamera == null)
            TargetCamera = Camera.main;
        if (TargetUI == null)
            TargetUI = gameObject.transform;

        // �eUI��RectTransform��ێ�
        ParentUI = TargetUI.parent.GetComponent<RectTransform>();

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
        if (TargetCamera == null)
            TargetCamera = Camera.main;
        if (TargetUI == null)
            TargetUI = gameObject.transform;

        // �eUI��RectTransform��ێ�
        ParentUI = TargetUI.parent.GetComponent<RectTransform>();
    }
    // UI�̈ʒu�𖈃t���[���X�V
    private void Update()
    {
        Des();
        OnUpdatePosition();
    }
    // UI�̈ʒu���X�V����
    private void OnUpdatePosition()
    {
        var cameraTransform = TargetCamera.transform;

        // �J�����̌����x�N�g��
        var cameraDir = cameraTransform.forward;
        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = Target.position + WorldOffset;
        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - cameraTransform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        //3D�Q�[���̏ꍇ�K�v
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        TargetUI.gameObject.SetActive(isFront);
        if (!isFront) return;

        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        var targetScreenPos = TargetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ParentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        //��P�����ɂ́A�ϊ����RectTransform���[�J�����W�̐e���w�肵�܂��B

        //��Q�����ɂ́A�ϊ����̃X�N���[�����W���w�肵�܂��B

        //��R�����ɂ́ACanvas�Ɋ֘A����J�������w�肵�܂��BCanvas���I�[�o�[���C���[�h[1] �ꍇ��null���w�肵�Ȃ���΂����܂���B

        //��S�����ɂ́ARectTransform�̃��[�J�����W���󂯎�邽�߂̕ϐ����w�肵�܂��B
        );

        // RectTransform�̃��[�J�����W���X�V
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
