using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public bool imStart;
    public bool imNothing;

    public LayerMask startLayer;
    public LayerMask nothingLayer;

    public GameManager gm;

    public bool selected = false;

    private Rigidbody2D rb;
    //private SpriteRenderer sprite;

    public float moveSpeed = 1f;
    //public bool faceLeft = false;

    public float jumpForce = 10f;
    public float groundCheckRange = 1f;
    private bool canJump;
    public bool onGround;

    RaycastHit2D groundCheck;

    public float eatCheckRange = 1f;
    RaycastHit2D eatCheck;
    public LayerMask edibleLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
     
    private void Update()
    {
        eatCheck = Physics2D.Raycast(transform.position, -Vector2.up, eatCheckRange, edibleLayer);

        if (selected && !gm.gameEnd)
        {
            if (imStart)
            {
                groundCheck = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckRange, startLayer);
            }

            if (imNothing)
            {
                groundCheck = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckRange, nothingLayer);
            }

            canJump = false;

            if (groundCheck.collider)
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }

            if (onGround)
            {
                canJump = true;
            }
        }

        //Power
        if (imNothing)
        {
            if (eatCheck)
            {
                if (eatCheck.collider != null)
                {
                    Debug.Log(eatCheck.collider.gameObject);
                }
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (selected && !gm.gameEnd)
        {
            //Horizontal
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, rb.velocity.y);

            //Jump
            if (Input.GetButtonDown("Jump") && canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}
