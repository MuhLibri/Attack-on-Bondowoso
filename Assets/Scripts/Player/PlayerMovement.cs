using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public float speed;
    public float turnSpeed;
    public float jumpForce;
    public float gravityMultiplier;
    public LayerMask groundLayer;

    bool onGround;
    bool jumpAllowed;
    Vector3 direction;
    Rigidbody rb;
    CapsuleCollider collider;



    // Start is called before the first frame update
    void Start()
    {

        // initialize the rigidbody and direction
        rb = GetComponent<Rigidbody>();
        direction = Vector3.zero;
        jumpAllowed = true;
        collider = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true; // so that the player dont topple over
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        //Debug.Log("FixedUpdate");
        groundCheck();

        jumpAllowed = onGround;

        Move();
        Jump();
        Turn();
        if(!onGround) {
            gravityScale();
        }
    }

    void Move() {
        // get the input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log("horizontal: " + horizontal + " vertical: " + vertical);

        // set the direction
        direction = transform.right * horizontal + transform.forward * vertical;

        // move the player
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        
    }

    void Turn() {
        
    }

    void Jump() {
        // take input
        if (Input.GetKey(KeyCode.Space) && jumpAllowed) {
            // add force to the player
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void gravityScale() {
        rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
    }

    void groundCheck(){
        float distance = collider.height / 2 + 0.5f;

        onGround = Physics.Raycast(transform.position,
                                    Vector3.down,
                                    distance,
                                    groundLayer);

        Debug.Log("onGround: " + onGround);
    }
}
