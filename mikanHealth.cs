using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mikanHealth : MonoBehaviour
{
    public float mikanExp { get; private set; }

    private GameManager GameM;

    [SerializeField] private float MikanHp, EnemyExp;

    private Slider Hpslider;

    [SerializeField]private string AttName = "Attack";//çUåÇîªíËÇÃtagÇÃñºëO

    
    void Start()
    {
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Hpslider = GameObject.Find("EnemySlider").GetComponent<Slider>();
        Hpslider.maxValue = MikanHp;
    }

   
    void FixedUpdate()
    {
        Hpslider.value = MikanHp;

        if (MikanHp <= 0)
        {
            mikanExp += EnemyExp;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == AttName)
        {
            MikanHp -= GameM.PlyerAttckD;
            Debug.Log(MikanHp);
        }
    }
}
