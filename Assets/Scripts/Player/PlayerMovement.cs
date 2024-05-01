using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public float speed;
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
    // Create a ray from the camera through the mouse position
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    // Create a Plane object at the player's Y position, with normal pointing up
    Plane plane = new Plane(Vector3.up, transform.position.y);

    // Determine where the ray intersects with the plane
    float enter;
    if (plane.Raycast(ray, out enter)) {
        // This is the world position at the mouse cursor
        Vector3 hitPoint = ray.GetPoint(enter);

        // Create a vector from the player to the hit point
        Vector3 lookDirection = hitPoint - transform.position;

        // Ensure the look direction is flat
        lookDirection.y = 0f;

        // Create a quaternion that looks in the direction of the hit point
        Quaternion newLookDirection = Quaternion.LookRotation(lookDirection);

        // Rotate the player to face the hit point
        rb.MoveRotation(newLookDirection);
    }
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
