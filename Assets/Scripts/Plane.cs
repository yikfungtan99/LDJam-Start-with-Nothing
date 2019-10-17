using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float speed;
    private float mspd;

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(mspd * Time.deltaTime, 0));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Start")
        {
            if (collision.gameObject.GetComponent<pStart>().startStuff)
            {
                mspd = speed;
            }
        }
    }

}
