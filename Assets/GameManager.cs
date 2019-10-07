using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject camMap;
    public GameObject camStart;
    public GameObject camNothing;

    private GameObject lastCam;

    public GameObject pStart, pNothing;
    public checker checkStart, checkNothing;

    public bool startExists;

    private bool startSelected, nothingSelected;

    public bool startReached, nothingReached;

    public bool gameEnd = false;

    public float goalTime;
    private float goalTimeCounter;


    // Start is called before the first frame update
    void Start()
    {
        startSelected = false;
        nothingSelected = false;

        pStart.GetComponent<pStart>().selected = false;
        pNothing.GetComponent<Nothing>().selected = false;

        camMap.SetActive(true);
        camStart.SetActive(false);
        camNothing.SetActive(false);

        startExists = true;

        lastCam = camMap;

        goalTimeCounter = goalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!pStart.GetComponent<pStart>().selected && !pNothing.GetComponent<Nothing>().selected)
            {
                startSelected = true;
                CameraChange(camStart);
            }

            if (pStart.GetComponent<pStart>().selected)
            {
                startSelected = false;
                nothingSelected = true;

                CameraChange(camNothing);
            }

            if(pNothing.GetComponent<Nothing>().selected && startExists)
            {
                startSelected = true;
                nothingSelected = false;
                
                CameraChange(camStart);
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            CameraChange(camMap);
        }

        if (startExists)
        {
            pStart.GetComponent<pStart>().selected = startSelected;
        }
        
        pNothing.GetComponent<Nothing>().selected = nothingSelected;

        //win condition

        if(startReached && nothingReached)
        {
            if(goalTimeCounter <= 0)
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
    }

    void CameraChange(GameObject cam)
    {
        if(cam != lastCam)
        {
            cam.SetActive(true);
            lastCam.SetActive(false);
            lastCam = cam;
        }
    }
}
