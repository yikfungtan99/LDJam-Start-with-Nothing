using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checker : MonoBehaviour
{

    public bool checkStart, checkNothing;

    [SerializeField]
    public bool reached;

    // Start is called before the first frame update
    void Start()
    {
        reached = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkStart)
        {
            if (collision.gameObject.tag == "Start")
            {
                reached = true;
            }
        }

        if (checkNothing)
        {
            if (collision.gameObject.tag == "Nothing" && !collision.gameObject.GetComponent<Nothing>().isSomething)
            {
                reached = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (checkStart)
        {
            if (collision.gameObject.tag == "Start")
            {
                reached = false;
            }
        }

        if (checkNothing)
        {
            if (collision.gameObject.tag == "Nothing")
            {
                reached = false;
            }
        }
    }
}
