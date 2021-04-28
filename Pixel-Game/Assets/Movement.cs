using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject pixelParent;

    [SerializeField]
    private LayerMask groundLayers;

    private Animator animator;

    Rigidbody2D body;
    Collider2D coll;

    public Transform foot;

    float horizontal;
    float vertical;

    public float speed = 0;

    private Vector3 currentVelocity = Vector3.zero;

    public float movementSmoothing = 0;

    bool jump = false;

    public Vector2 footOverlapCapsuleOffset;

    public Vector2 longJumpForce = Vector2.zero;
    public Vector2 highJumpForce = Vector2.zero;
    public Vector2 modifierJumpForce = Vector2.zero;

    bool isGrounded = false;

    int faceDirection = 1;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            //print("asdasda");
            jump = true;
        }


        if (horizontal > 0)
        {
            faceDirection = 1;
        }
        else if (horizontal < 0){
            faceDirection = -1;
        }
       

        if (horizontal != 0 && isGrounded)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        if (faceDirection == -1)
        {
            spriteRenderer.flipX = true;
            pixelParent.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            spriteRenderer.flipX = false;
            pixelParent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //print(faceDirection);

        if (isGrounded)
        {
            animator.SetBool("inAir", false);
        }
        else
        {
            animator.SetBool("inAir", true);
        }


        /*if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine("Explode");


        }*/

    }

    IEnumerator Explode()
    {
        Vector2 velocity = body.velocity;

        coll.enabled = false;
        body.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(1);

        pixelParent.transform.parent = null;

        gameObject.SetActive(false);

        pixelParent.GetComponent<ExplodeController>().Explode(velocity);

    }


    private void FixedUpdate()
    {

        Collider2D[] groundColliders = Physics2D.OverlapCapsuleAll(foot.position + (Vector3)footOverlapCapsuleOffset, coll.bounds.size * 0.9f, CapsuleDirection2D.Horizontal, 0, groundLayers);

        isGrounded = false;

        foreach (Collider2D a in groundColliders)
        {
            if (a.gameObject != gameObject)
            {
                isGrounded = true;
                break;
            }
        }

       

        //print(isGrounded);

        if (isGrounded && jump)
        {

            
            if (Input.GetButton("Fire3") || Input.GetAxis("Fire3") != 0)
            {
                body.AddForce(new Vector2(modifierJumpForce.x * faceDirection, modifierJumpForce.y));
            }
            else
            {
                if (horizontal != 0)
                {
                    //Long Jump
                    body.AddForce(new Vector2(longJumpForce.x * faceDirection, longJumpForce.y));
                }
                else
                {
                    //High Jump
                    body.AddForce(new Vector2(highJumpForce.x * faceDirection, highJumpForce.y));
                }
            }

            

            

        }

        jump = false;

        if (isGrounded)
        {
            Vector3 targetVelocity = new Vector3(horizontal * speed * Time.deltaTime, body.velocity.y, 0);

            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
        }
       

    


    }


}
