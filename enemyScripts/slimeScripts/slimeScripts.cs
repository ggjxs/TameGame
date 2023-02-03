using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeScripts : MonoBehaviour
{
    Rigidbody rd;
    PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        PC = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void GetPosition()
    {
        var PositionSlime = rd.position;
        var PositionPlayer = PC.PositionGet;
    }
}
