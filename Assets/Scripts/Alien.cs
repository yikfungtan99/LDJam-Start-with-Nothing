using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed;
    public float mspd;

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, mspd * Time.deltaTime));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Start")
        {
            mspd = speed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Start")
        {
            mspd = 0;
        }
    }

}
