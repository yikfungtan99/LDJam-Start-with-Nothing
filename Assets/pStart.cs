using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pStart : MonoBehaviour
{
    public LayerMask startLayer;

    public GameManager gm;

    public bool selected = false;

    private Rigidbody2D rb;
    //private SpriteRenderer sprite;

    public float moveSpeed = 1f;
    //public bool faceLeft = false;

    public float jumpForce = 10f;
    public float groundCheckRange = 1f;
    public bool onGround;
    public bool onPlane;

    public float planeSpeed;

    RaycastHit2D groundCheck;
    RaycastHit2D planeCheck;

    public LayerMask planeLayer;
    public float planeCheckRange;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        groundCheck = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckRange, startLayer);
        planeCheck = Physics2D.Raycast(transform.position, -Vector2.up, planeCheckRange, planeLayer);

        if (groundCheck.collider)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (planeCheck.collider)
        {
            onPlane = true;
            this.transform.parent = planeCheck.collider.transform;    
        }
    }

    private void FixedUpdate()
    {

        if (selected && !gm.gameEnd)
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
}
