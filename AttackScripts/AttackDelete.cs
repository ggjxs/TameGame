using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDelete : MonoBehaviour
{
    [SerializeField] private float DestroySpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,DestroySpeed);
    }
}
