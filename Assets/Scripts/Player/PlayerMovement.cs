using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float defaultSpeed = 5;
    public float turnSpeed;
    public float jumpForce;
    public float sprintMultiplier;
    public float gravityMultiplier;
    public LayerMask groundLayer;

    static float speed;
    bool onGround;
    bool jumpAllowed;
    public bool moveAllowed;
    Vector3 direction;
    Rigidbody rb;
    CapsuleCollider playerCollider;
    public Animator playerAnimator;
    Coroutine speedIncreaseCoroutine;

    public static bool twiceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // initialize
        rb = GetComponent<Rigidbody>();
        direction = Vector3.zero;
        jumpAllowed = true;
        playerCollider = GetComponent<CapsuleCollider>();
        speed = defaultSpeed;

        rb.freezeRotation = true; // so that the player dont topple over
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (GetComponent<PlayerHealth>().currentHealth > 0)
        {
            groundCheck();

            jumpAllowed = onGround;
            playerAnimator.SetBool("Fall", !onGround);

            Move();
            Jump();
            Turn();

            if (!onGround)
            {
                gravityScale();
            }
        }
    }

    void Move()
    {
        // get the input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float moveSpeed = speed;

        //Debug.Log("horizontal: " + horizontal + " vertical: " + vertical);

        // set the direction
        direction = transform.right * horizontal + transform.forward * vertical;

        // sprint multiplier
        if (Input.GetKey(KeyCode.LeftShift) && (vertical > 0))
        {
            moveSpeed *= sprintMultiplier;
            playerAnimator.SetBool("Sprint", true);
        }
        else
        {
            playerAnimator.SetBool("Sprint", false);
        }


        // set animation parameters
        playerAnimator.SetBool("Forward", false);
        playerAnimator.SetBool("Backward", false);
        playerAnimator.SetBool("Right", false);
        playerAnimator.SetBool("Left", false);

        if (horizontal > 0)
        {
            // Debug.Log("Moving Right");
            playerAnimator.SetBool("Right", true);
        }
        else if (horizontal < 0)
        {
            // Debug.Log("Moving Left");
            playerAnimator.SetBool("Left", true);
        }
        else if (vertical > 0)
        {
            // Debug.Log("Moving Forward");
            playerAnimator.SetBool("Forward", true);

        }
        else if (vertical < 0)
        {
            // Debug.Log("Moving Backward");
            playerAnimator.SetBool("Backward", true);
        }

        // move the player
        if (moveAllowed)
        {
            {
                Vector3 previousPosition = transform.position;
                rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
                Vector3 currentPosition = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
                float distanceThisFrame = Vector3.Distance(currentPosition, previousPosition);
                StatisticsManager.Instance.UpdateDistanceTraveled(distanceThisFrame);
            }
        }
    }

    void Turn()
    {

    }

    void Jump()
    {
        // take input
        if (Input.GetKey(KeyCode.Space) && jumpAllowed)
        {
            // add force to the player
            Debug.Log("Jump");
            playerAnimator.SetTrigger("JumpUp");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void gravityScale()
    {
        rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
    }

    void groundCheck()
    {
        float distance = playerCollider.height / 2 + 0.5f;

        onGround = Physics.Raycast(transform.position,
                                    Vector3.down,
                                    distance,
                                    groundLayer);

        // Debug.Log("onGround: " + onGround);
    }

    IEnumerator SpeedIncrease(float duration, int boostPercentage)
    {

        speed += (speed * (boostPercentage / 100f));
        Debug.Log("Speed Increased to: " + speed);
        Debug.Log("Speed Increase Percentage: " + boostPercentage);
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
        Debug.Log("Speed Decreased to: " + speed);
        speedIncreaseCoroutine = null;
    }

    public void startSpeedIncrease(float duration, int boostPercentage)
    {
        if (speedIncreaseCoroutine != null)
        {
            StopCoroutine(speedIncreaseCoroutine);
        }
        speedIncreaseCoroutine = StartCoroutine(SpeedIncrease(duration, boostPercentage));
    }

    public void SpeedDecrease(int percentage)
    {
        speed -= (speed * (percentage / 100f));
    }

    public void ResetSpeed()
    {
        speed = defaultSpeed;
    }

    public static bool IsTwiceSpeed()
    {
        return twiceSpeed;
    }

    public static void ActivateTwiceSpeed()
    {
        twiceSpeed = true;
        speed *= 2;
    }

    public static void DeactivateTwiceSpeed()
    {
        twiceSpeed = false;
        speed /= 2;
    }
}
