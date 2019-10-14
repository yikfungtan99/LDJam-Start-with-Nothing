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

    /**
    private Rigidbody2D rb;
    public float moveSpeed = 1f;
    //public bool faceLeft = false;

    public float jumpForce = 10f;
    public float groundCheckRange = 1f;
    public bool onGround;
    public bool onPlane;
    public float planeSpeed;

    Collider2D groundCheckBox;
    RaycastHit2D planeCheck;

    public LayerMask planeLayer;
    public float planeCheckRange;

    public bool onAlien;
    **/

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
    public Sprite sprNothing;

    // Start is called before the first frame update
    void Start()
    {
        //rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        eatCheck = Physics2D.Raycast(transform.position, -Vector2.up, eatCheckRange, edibleLayer);
        placeCheck = Physics2D.Raycast(transform.position, -Vector2.up, placeCheckRange, placeableLayer);
        //groundCheckBox = Physics2D.OverlapArea(new Vector2(transform.position.x - 1, transform.position.y), new Vector2(transform.position.x + 1, transform.position.y - groundCheckRange), nothingLayer);
        //planeCheck = Physics2D.Raycast(transform.position, -Vector2.up, planeCheckRange, planeLayer);
        
        /**
        if (planeCheck.collider)
        {
            if(planeCheck.collider.tag == "Plane")
            {
                onPlane = true;
                this.transform.parent = planeCheck.collider.transform;
            }

            if(planeCheck.collider.tag == "Alien")
            {
                onAlien = true;
                this.transform.parent = planeCheck.collider.transform;
            }
           
        }
        else
        {
            this.transform.SetParent(null);
        }

        if (groundCheckBox)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        **/

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
           
        }

        if (haveItem)
        {
            this.GetComponent<SpriteRenderer>().sprite = null;
            someThing.SetActive(true);
            isSomething = true;
        }
        if (!haveItem)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprNothing;
            someThing.SetActive(false);
            isSomething = false;
        }
    }

    void FixedUpdate()
    {
        /**
        if (isSelected && !gm.gameEnd)
        {
            //Horizontal
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, rb.velocity.y);

            //Jump
            if (Input.GetButtonDown("Jump") && onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        if (!isSelected && !gm.gameEnd)
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
        **/
    }

    void Swallow(GameObject target)
    {
        haveItem = true;

        if(target.tag == "Start")
        {
            gm.startExists = false;
        }

        if(target.tag == "Plane")
        {
            if (this.GetComponent<Player>().onPlane)
            {
                transform.SetParent(null);
            }

            if (gm.pStart.GetComponent<Player>().onPlane)
            {
                gm.pStart.transform.SetParent(null);
            }
            
        }

        if(target.tag == "Alien")
        {
            if (this.GetComponent<Player>().onAlien)
            {
                transform.SetParent(null);
            }

            if (gm.pStart.GetComponent<Player>().onAlien)
            {
                gm.pStart.transform.SetParent(null);
            }
            
        }

        target.SetActive(false);

        Instantiate(ghostToPlace, target.transform.position, target.transform.rotation);
    }

    void Place(GameObject target)
    {
        haveItem = false;
        
        if (item.tag == "Start")
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }

        item.SetActive(true);
        item.transform.position = target.transform.position;
        item.transform.rotation = target.transform.rotation;
        //Instantiate(itemToPlace, target.transform.position, target.transform.rotation);
        Destroy(target);
    }

    void ItemCheck()
    {
        for (int i = 0; i < bl.blocks.Length; i++)
        {
            if (item.tag == bl.blocks[i].name)
            {
                itemToPlace = bl.blocks[i];
                ghostToPlace = bl.ghosts[i];
            }
        }
    }

    /**
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x - 1, transform.position.y), new Vector2(transform.position.x + 1, transform.position.y - groundCheckRange));
    }
    **/
}
