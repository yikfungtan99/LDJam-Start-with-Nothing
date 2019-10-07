using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nothing : MonoBehaviour
{
    public bool isSomething;
    public LayerMask nothingLayer;

    public GameManager gm;
    public blockLibrary bl;

    public bool selected = false;

    private Rigidbody2D rb;
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

    public GameObject someThing;
    private Sprite sprNothing;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sprNothing = this.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        eatCheck = Physics2D.Raycast(transform.position, -Vector2.up, eatCheckRange, edibleLayer);
        placeCheck = Physics2D.Raycast(transform.position, -Vector2.up, placeCheckRange, placeableLayer);
        groundCheck = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckRange, nothingLayer);
        planeCheck = Physics2D.Raycast(transform.position, -Vector2.up, planeCheckRange, planeLayer);

        if (planeCheck.collider)
        {
            onPlane = true;
            this.transform.parent = planeCheck.collider.transform;
        }
        if (groundCheck.collider)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (selected && !gm.gameEnd)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!haveItem)
                {
                    if (eatCheck)
                    {
                        if (eatCheck.collider != null)
                        {

                           item = eatCheck.collider.transform.parent.gameObject;     
                           ItemCheck();     
                           Swallow(eatCheck.collider.transform.parent.gameObject);     

                        }
                    }
                }
                else
                {
                    if (placeCheck)
                    {
                        if (placeCheck.collider != null)
                        {
                            Place(placeCheck.collider.transform.parent.gameObject);
                        }
                    }
                }
            }

            if (haveItem)
            {
                this.GetComponent<SpriteRenderer>().sprite = null;
                someThing.SetActive(true);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = sprNothing;
                someThing.SetActive(false);
            }
        }
    }

    void FixedUpdate()
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

        if (!selected && !gm.gameEnd)
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

    void Swallow(GameObject target)
    {
        haveItem = true;

        if(target.tag == "Start")
        {
            gm.startExists = false;
        }

        target.SetActive(false);

        Instantiate(ghostToPlace, target.transform.position, target.transform.rotation);
    }

    void Place(GameObject target)
    {
        haveItem = false;
        Destroy(target);

        if (item.tag == "Start")
        {
            gm.startExists = true;
        }

        item.SetActive(true);
        item.transform.position = target.transform.position;
        item.transform.rotation = target.transform.rotation;
        //Instantiate(itemToPlace, target.transform.position, target.transform.rotation);
    }
    void ItemCheck()
    {
        for (int i = 0; i < bl.blocks.Length; i++)
        {
            if (item.name == bl.blocks[i].name)
            {
                //itemToPlace = bl.blocks[i];
                ghostToPlace = bl.ghosts[i];
            }
        }
    }
}
