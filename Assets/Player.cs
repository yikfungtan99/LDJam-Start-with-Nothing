using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gm;

    public float moveSpeed = 1f;
    public float jumpForce = 10f;
    public float groundCheckRange = 1f;
    public bool onGround;
    public bool onPlane;
    public bool onAlien;
    //public float planeSpeed;

    private Rigidbody2D rb;
    Collider2D groundCheckBox;
    RaycastHit2D planeCheck;
    public LayerMask planeLayer;
    public float planeCheckRange;
    private bool isSelected;
    private LayerMask myLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<pStart>() != null)
        {
            isSelected = this.GetComponent<pStart>().selected;
            myLayer = this.GetComponent<pStart>().startLayer;
        }

        if (this.GetComponent<Nothing>() != null)
        {
            isSelected = this.GetComponent<Nothing>().selected;
            myLayer = this.GetComponent<Nothing>().nothingLayer;
        }


        groundCheckBox = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.7f, transform.position.y), new Vector2(transform.position.x + 0.35f, transform.position.y - groundCheckRange), myLayer);
        planeCheck = Physics2D.Raycast(transform.position, -Vector2.up, planeCheckRange, planeLayer);

        if (groundCheckBox)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (planeCheck.collider)
        {
            if (planeCheck.collider.tag == "Plane")
            {
                onPlane = true;
                this.transform.parent = planeCheck.collider.transform;
            }

            if (planeCheck.collider.tag == "Alien")
            {
                onAlien = true;
                this.transform.parent = planeCheck.collider.transform;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
        }
        else
        {
            this.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {

        if (!gm.gameEnd && isSelected)
        {
            //Horizontal
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, rb.velocity.y);

            //Jump
            if (Input.GetButtonDown("Jump") && onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

        }
        else
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2((rb.velocity.x - 2 * Time.deltaTime), rb.velocity.y);
            }

            if (rb.velocity.x <= 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        if (gm.gameEnd)
        {
            rb.velocity = new Vector3(0, rb.velocity.x);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x - 0.7f, transform.position.y), new Vector2(transform.position.x + 0.35f, transform.position.y - groundCheckRange));
    }
}
