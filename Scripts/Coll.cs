using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coll : MonoBehaviour
{
    private CircleCollider2D Cir2D;
    private string AttName = "Attack", PlayerName = "Player";

    // Start is called before the first frame update
    void Start()
    {
        Cir2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == PlayerName)
            Cir2D.enabled = true;
        else if (collision.tag == AttName)
            Cir2D.enabled = false;
        else
            Cir2D.enabled = true;
    }
}
