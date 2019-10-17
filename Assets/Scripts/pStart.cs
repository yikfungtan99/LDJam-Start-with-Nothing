using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pStart : MonoBehaviour
{
    public LayerMask startLayer;
    private GameManager gm;

    public bool selected = false;
    public bool eaten = false;

    //RaycastHit2D startCheck;
    public float startCheckRange = 1f;
    public LayerMask startCheckLayer;

    public float startTime = 1f;
    private float startTimeCounter = 1f;
    public bool startStuff = false;

    //public string jump_sound;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        //startCheck = Physics2D.Raycast(transform.position, -Vector2.up, startCheckRange, startCheckLayer);

        if (selected && !gm.gameEnd)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                startStuff = true;
            }

            if (startStuff)
            {
                if(startTimeCounter <= 0)
                {
                    startStuff = false;
                    startTimeCounter = startTime;

                }
                else
                {
                    startTimeCounter -= Time.deltaTime;
                }
            }
        }

    }

    private void OnBecameInvisible()
    {
        if (!eaten)
        {
            gm.startDead = true;
        }
    }
}
