using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sikai : MonoBehaviour
{
    
    public bool ColliderTrigger { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        ColliderTrigger = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            ColliderTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag=="Player")
        {
            ColliderTrigger = false;
        }
    }

}
