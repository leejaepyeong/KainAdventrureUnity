using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    

    public void StartClick()
    {
        SceneManager.LoadScene("Advanture");
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
