using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_R_Player : MonoBehaviour
{
    [SerializeField]
    GameObject Canvas;
    private Button_R_Canvas CanvasScripts;
    public int RandomCount { get; set; }

    public bool TimeStop { get; private set; } = false;

    public bool CanvasTrigger { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
        CanvasScripts = Canvas.GetComponent<Button_R_Canvas>();
        Canvas.SetActive(false);
        CanvasTrigger = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && CanvasTrigger == false)
        {
            TimeStop = true;
            Time.timeScale = 0;
            CanvasTrigger = true;
            Canvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.R) && CanvasTrigger == true)
        {
            TimeStop = false;
            Time.timeScale = 1;
            CanvasTrigger = false;
            RandomCount = 0;
            Canvas.SetActive(false);
        }


    }
}
