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

    private bool startSelected, nothingSelected;

    public bool startReached, nothingReached;

    public bool gameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        startSelected = false;
        nothingSelected = false;

        pStart.GetComponent<playerController>().selected = true;
        pNothing.GetComponent<playerController>().selected = false;

        camMap.SetActive(true);
        camStart.SetActive(false);
        camNothing.SetActive(false);

        lastCam = camMap;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!pStart.GetComponent<playerController>().selected && !pNothing.GetComponent<playerController>().selected)
            {
                startSelected = true;
                CameraChange(camStart);
            }

            if (pStart.GetComponent<playerController>().selected)
            {
                startSelected = false;
                nothingSelected = true;

                CameraChange(camNothing);
            }

            if(pNothing.GetComponent<playerController>().selected)
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

        pStart.GetComponent<playerController>().selected = startSelected;
        pNothing.GetComponent<playerController>().selected = nothingSelected;

        //win condition

        if(startReached && nothingReached)
        {
            gameEnd = true;
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
