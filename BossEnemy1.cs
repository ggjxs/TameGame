using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy1 : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private float StartPos;

    void Start()
    {
        //Mathf.PingPong(éûä‘, è„â∫ç∑);
        //Mathf.PingPong(time.deltaTime, 10);Å@0Å`10ÇÃílÇ™ï‘Ç¡ÇƒÇ≠ÇÈ



        StartPos = this.transform.position.y;
    }
    
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, StartPos + Mathf.PingPong(Time.time, Speed), transform.position.z);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
      
    }
    



}
