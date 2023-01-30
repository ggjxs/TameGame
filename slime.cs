using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    private GameManager GameM;


    [SerializeField]private float SlimeHp;
    [SerializeField] GameObject Player,sikai;
    Rigidbody2D rd;
    Ray2D ray;
    sikai sikaiSlime;
    LayerMask layerMask;

    private string Att = "Attack";

    [SerializeField] private float speed;
    Vector2 RayDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        GameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        sikaiSlime = sikai.GetComponent<sikai>();

        rd = GetComponent<Rigidbody2D>();
        RayDirection = Player.transform.position;

        
        layerMask = LayerMask.GetMask("Player&folor");
        Debug.Log(layerMask);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sikaiSlime.ColliderTrigger == true)
        {
            RayDirection = new Vector2(Player.transform.position.x - rd.position.x, Player.transform.position.y - rd.position.y);
            RaycastHit2D hit = Physics2D.Raycast(rd.transform.position, RayDirection,10.0f,layerMask);
            Debug.DrawRay(rd.position, RayDirection, Color.red);

            if (hit)
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed);
        }
        
    }
    void Update()
    {
        
        if (SlimeHp <= 0)
        {
            Destroy(gameObject);
        }
           
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Att)
        {
            SlimeHp -= GameM.PlyerAttckD;
            Debug.Log(SlimeHp);
        }
    }
}
