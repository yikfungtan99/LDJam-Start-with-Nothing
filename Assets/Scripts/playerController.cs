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
    public blockLibrary bl;

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
    private GameObject item;

    public bool haveItem = false;
    public float placeCheckRange = 1f;
    RaycastHit2D placeCheck;
    public LayerMask placeableLayer;

    private GameObject itemToPlace;
    private GameObject ghostToPlace;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
     
    private void Update()
    {
        eatCheck = Physics2D.Raycast(transform.position, -Vector2.up, eatCheckRange, edibleLayer);
        placeCheck = Physics2D.Raycast(transform.position, -Vector2.up, placeCheckRange, placeableLayer);

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
                    if (Input.GetKeyDown(KeyCode.F) && !haveItem)
                    {
                        item = eatCheck.collider.transform.parent.gameObject;
                        ItemCheck();
                        Swallow(eatCheck.collider.transform.parent.gameObject);
                    }
                }
            }

            if (placeCheck)
            {
                if (placeCheck.collider != null)
                {
                    if (Input.GetKeyDown(KeyCode.F) && haveItem)
                    {
                        Place(placeCheck.collider.transform.parent.gameObject);
                    }
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

        if(!selected && !gm.gameEnd)
        {
            if(rb.velocity.x > 0)
            {
                rb.velocity = new Vector2((rb.velocity.x - 2 * Time.deltaTime), rb.velocity.y);
            }

            if (rb.velocity.x <= 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    void Swallow(GameObject target)
    {
        haveItem = true;
        Destroy(target);
        Instantiate(ghostToPlace, target.transform.position, target.transform.rotation);    
    }

    void Place(GameObject target)
    {
        haveItem = false;
        Destroy(target);
        Instantiate(itemToPlace, target.transform.position, target.transform.rotation);
    }

    void ItemCheck()
    {
        for(int i = 0; i < bl.blocks.Length; i++)
        {
            if (item.name == bl.blocks[i].name)
            {
                itemToPlace = bl.blocks[i];
                ghostToPlace = bl.ghosts[i];
            }
        }
    }
}
