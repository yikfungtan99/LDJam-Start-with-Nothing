using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float speed;
    public float mspd;

    Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(mspd * Time.deltaTime, 0));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Start")
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
