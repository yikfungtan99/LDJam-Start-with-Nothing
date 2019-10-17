using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private SpriteRenderer spr;

    private void Start()
    {
        spr = this.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "EatChecker"))
        {
            spr.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "EatChecker"))
        {
            spr.enabled = false;
        }
    }
}
