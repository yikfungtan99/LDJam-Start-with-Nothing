using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject hiLite;
    public GameObject hiLiteN;

    public void Highlight()
    {
        if(hiLite.active == false)
        {
            hiLite.SetActive(true);
        }
        else
        {
            hiLite.SetActive(false);
        }   
    }

    public void HighlightN()
    {
        if (hiLiteN.active == false)
        {
            hiLiteN.SetActive(true);
        }
        else
        {
            hiLiteN.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
