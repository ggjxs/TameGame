using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sikai : MonoBehaviour
{
    GameObject Root;
    Vector2 PositionRoot, PositionFormarrRoot;

    [SerializeField] GameObject player;
    public bool ColliderTrigger{ get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Root = transform.root.gameObject;
        transform.position = Root.transform.position;
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
