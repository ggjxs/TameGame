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

    // �o�ߎ���,
    private float time;

    private void Start()
    {
        Canvas = GameObject.Find("Canvas").transform;

        //// rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
        //float x = Random.Range(rangeA.position.x, rangeB.position.x);
        //// rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
        //float y = Random.Range(rangeA.position.y, rangeB.position.y);
        //// rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
        //float z = Random.Range(rangeA.position.z, rangeB.position.z);

        ////GameObject Slider = Instantiate(UIPrefab, Vector3.zero, Quaternion.identity);
        ////Slider.transform.SetParent(Canvas);

        //// GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
        //GameObject Mikan = Instantiate(Prefab, new Vector3(x, y, z), Quaternion.identity);

       
    }

    void Update()
    {
        // �O�t���[������̎��Ԃ����Z���Ă���
        time += Time.deltaTime;

        // ��1�b�u���Ƀ����_���ɐ��������悤�ɂ���B
        if (time > IntervalTimer)
        {
            // rangeA��rangeB��x���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeA��rangeB��y���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float y = Random.Range(rangeA.position.y, rangeB.position.y);
            // rangeA��rangeB��z���W�͈͓̔��Ń����_���Ȑ��l���쐬
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            // GameObject����L�Ō��܂��������_���ȏꏊ�ɐ���
            var Mikan = Instantiate(Prefab, new Vector3(x, y, z), Prefab.transform.rotation);
           
            GameObject Slider = Instantiate(UIPrefab, Vector3.zero, Prefab.transform.rotation);

            Slider.transform.SetParent(Canvas);

            // �o�ߎ��ԃ��Z�b�g
            time = 0f;
        }
    }
}
