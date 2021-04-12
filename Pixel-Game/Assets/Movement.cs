using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private LayerMask groundLayers;

    private Animator animator;

    Rigidbody2D body;

    public Transform foot;

    float horizontal;
    float vertical;

    public float speed = 0;

    private Vector3 currentVelocity = Vector3.zero;

    public float movementSmoothing = 0;

    bool jump = false;

    public Vector2 longJumpForce = Vector2.zero;
    public Vector2 highJumpForce = Vector2.zero;

    bool isGrounded = false;

    int faceDirection = 1;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
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


        if (horizontal == 1)
        {
            faceDirection = 1;
        }
        else if (horizontal == -1){
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
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        //print(faceDirection);





    }

    private void FixedUpdate()
    {


        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(foot.position, 0.05f, groundLayers);

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

        jump = false;

        if (isGrounded)
        {
            Vector3 targetVelocity = new Vector3(horizontal * speed * Time.deltaTime, body.velocity.y, 0);

            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
        }
       

    


    }


}
