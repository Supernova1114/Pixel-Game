using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayers;

    public bool canStick = false;
    public bool playerSticking = false;

    private Vector3 currentVelocity = Vector3.zero;

    public Vector2 movementForce = Vector2.zero;
    public Vector2 jumpForce;
    public float speed = 0;
    public float movementSmoothing = 0;

    public Rigidbody2D body;
    public Movement movementController;

    private bool isGrounded = false;

    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //print("asdasda");
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(transform.position, 0.15625f, groundLayers);//fixme

        isGrounded = false;

        foreach (Collider2D a in groundColliders)
        {
            if (a.gameObject != gameObject)
            {
                isGrounded = true;
                break;
            }
        }

        print(isGrounded);

        if (playerSticking && isGrounded)
        {
            /*body.AddForce(movementForce, 0);*/
            Vector3 targetVelocity = new Vector3(movementController.horizontal * speed * Time.deltaTime, body.velocity.y, 0);

            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

            if (movementController.jump)
            {
                body.AddForce(jumpForce, ForceMode2D.Impulse);
            }

        }
        jump = false;
    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }



}
