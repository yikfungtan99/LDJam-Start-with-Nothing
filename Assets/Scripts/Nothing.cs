using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nothing : MonoBehaviour
{
    public bool isSomething;
    public LayerMask nothingLayer;

    private GameManager gm;
    public blockLibrary bl;

    public bool selected = false;

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

    //public string jump_sound;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Find("eatChecker").gameObject.SetActive(selected);

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
            if (eatCheck)
            {
                if (!haveItem)
                {
                    if (eatCheck.collider != null)
                    {
                        if (eatCheck.collider.transform.parent.gameObject.transform.Find("Highlight") != null)
                        {
                            eatCheck.collider.transform.parent.gameObject.transform.Find("Highlight").gameObject.SetActive(true);
                        }
                        
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            item = eatCheck.collider.transform.parent.gameObject;
                            ItemCheck();

                            Swallow(eatCheck.collider.transform.parent.gameObject);
                        }
                    }
                }     
            }

            if (placeCheck)
            {
                if (haveItem)
                {
                    if (placeCheck.collider != null)    
                    {    
                        if (Input.GetKeyDown(KeyCode.F))    
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
            target.GetComponent<pStart>().eaten = true;
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
        
        item.SetActive(true);

        if (item.tag == "Start")
        {
            item.GetComponent<pStart>().eaten = false;
            gm.startExists = true;
        }

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

    private void OnBecameInvisible()
    {
        gm.nothingDead = true;
    }
}
