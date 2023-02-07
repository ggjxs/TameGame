using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
    void _ReStart()
    {
        var Scenecurrent = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Scenecurrent);
    }
}
