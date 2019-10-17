using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioManager am;

    public GameObject camMap;
        
    /**
    public GameObject camStart;
    public GameObject camNothing;

    private GameObject lastCam;
    **/

    public GameObject pStart, pNothing;
    public checker checkStart, checkNothing;

    public bool startExists;

    private bool startSelected, nothingSelected;

    public bool startReached, nothingReached;

    public bool gameEnd = false;
    public bool gameRestart = false;

    public float goalTime;
    private float goalTimeCounter;

    public GameObject nextLevel;
    public GameObject help;

    public bool nothingDead;
    public bool startDead;

    private void Awake()
    {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startExists = true;

        startSelected = true;
        nothingSelected = false;

        pStart.GetComponent<pStart>().selected = true;
        pNothing.GetComponent<Nothing>().selected = false;

        //camMap.SetActive(false);
        //camStart.SetActive(true);
        //camNothing.SetActive(false);

        //lastCam = camStart;
 
        goalTimeCounter = goalTime;

        gameRestart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (pStart.GetComponent<pStart>().selected)
            {
                startSelected = false;
                nothingSelected = true;

                //CameraChange(camNothing);
            }

            if (pNothing.GetComponent<Nothing>().selected && startExists)
            {
                startSelected = true;
                nothingSelected = false;
                
                //CameraChange(camStart);
            }
        }

        /**
        if (Input.GetKeyDown(KeyCode.M))
        {
            CameraChange(camMap);
        }
        **/

        if (startExists)
        {
            pStart.GetComponent<pStart>().selected = startSelected;
        }
        
        pNothing.GetComponent<Nothing>().selected = nothingSelected;

        //win condition

        if (startReached && nothingReached)
        {
            if (goalTimeCounter <= 0)
            {
                gameEnd = true;
                goalTimeCounter = goalTime;
            }
            else
            {
                goalTimeCounter -= Time.deltaTime;
            }
        }

        if (checkStart.reached)
        {
            startReached = true;
        }
        else
        {
            startReached = false;
        }

        if (checkNothing.reached)
        {
            nothingReached = true;
        }
        else
        {
            nothingReached = false;
        }

        if (gameEnd)
        {
            nextLevel.SetActive(true);
        }

        //lose condition
        if(startDead || nothingDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /**
    void CameraChange(GameObject cam)
    {
        if (cam != lastCam)
        {
            cam.SetActive(true);
            lastCam.SetActive(false);
            lastCam = cam;
        }
    }
    **/

    public void LevelChange()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseHelp()
    {
        if (help.active == true)
        {
            help.SetActive(false);
        }
        else
        {
            help.SetActive(true);
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
