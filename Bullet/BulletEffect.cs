using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    //�e�̑������ʂ̃X�N���v�g

    private Button_R_Canvas Bullet_R;

    private GameObject[] BulletMatching;

    void Start()
    {
        Bullet_R = GetComponent<Button_R_Canvas>();

        for (var i = 0; i > 5; i++)
            BulletMatching[i] = Bullet_R.BulletRandom[i];

    }

    
    void Update()
    {
        
    }
}
