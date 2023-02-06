using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public bool isground { get; private set; }

    private void Start()
    {
        isground = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
            isground = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
            isground = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isground = false;
    }
}
