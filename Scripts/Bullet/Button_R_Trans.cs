using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_R_Trans : MonoBehaviour
{
    private Button_R_Canvas CanvasScripts;
    
    void Start()
    {
        CanvasScripts = transform.root.gameObject.GetComponent<Button_R_Canvas>();
    }

    
    void Update()
    {

    }

    [SerializeField]
    public void Click()
    {
        CanvasScripts.SendMessage("ClickCountUp");

        if (CanvasScripts.Bullet1 == null)
        {
            CanvasScripts.Bullet1 = gameObject.name;
        }
        else if (CanvasScripts.Bullet1 == gameObject.name)
        {
            CanvasScripts.Bullet1 = null;
            CanvasScripts.SendMessage("ClickCountZero");
        }
        else
        {
            CanvasScripts.Bullet2 = gameObject.name;
        }


    }
}