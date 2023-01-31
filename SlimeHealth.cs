using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlimeHealth : MonoBehaviour
{
    public float SlimeExp { get; private set; }

    private GameManager GameM;

    [SerializeField] private float EnemyHp, EnemyExp;

    [SerializeField] private Slider Hpslider;

    [SerializeField] private string AttName = "Attack";//çUåÇîªíËÇÃtagÇÃñºëO


    void Start()
    {
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();        
        Hpslider.maxValue = EnemyHp;
    }


    void FixedUpdate()
    {
        Hpslider.value = EnemyHp;

        if (EnemyHp <= 0)
        {
            SlimeExp += EnemyExp;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == AttName)
        {
            EnemyHp -= GameM.PlyerAttckD;
        }
    }
}
