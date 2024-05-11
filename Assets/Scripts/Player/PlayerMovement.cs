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
    bool arrowPointerActive;
    public bool moveAllowed;
    Vector3 direction;
    Rigidbody rb;
    CapsuleCollider playerCollider;
    public Animator playerAnimator;
    Coroutine speedIncreaseCoroutine;

    // For Twice Speed Cheat
    public static bool twiceSpeed;
    private static float normalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // initialize
        rb = GetComponent<Rigidbody>();
        direction = Vector3.zero;
        jumpAllowed = true;
        playerCollider = GetComponent<CapsuleCollider>();
        speed = defaultSpeed;
        arrowPointerActive = true;
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
            GroundCheck();

            jumpAllowed = onGround;
            playerAnimator.SetBool("Fall", !onGround);

            Move();
            Jump();
            Turn();

            if (!onGround)
            {
                GravityScale();
            }
            Flairing();
        }
    }

    void Flairing(){
        if(Input.GetKey(KeyCode.F)){
            playerAnimator.SetBool("Flair", true);
            moveAllowed = false;
            jumpAllowed = false;
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            playerHealth.Heal(1);
        }
        else {
            playerAnimator.SetBool("Flair", false);
            moveAllowed = true;
            jumpAllowed = true;
        }
    }

    void Move()
    {
        // get the input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float moveSpeed = speed;

        // Debug.Log("horizontal: " + horizontal + " vertical: " + vertical);

        // set the direction
        direction = transform.right * horizontal + transform.forward * vertical;

        // Toggle arrow pointer
        if (Input.GetKeyDown(KeyCode.M))
        {
            ArrowPointer arrowPointer = GetComponentInChildren<ArrowPointer>();
            if (arrowPointerActive)
            {
                arrowPointer.ToggleArrow(false);
                arrowPointerActive = false;
            }
            else
            {
                arrowPointer.ToggleArrow(true);
                arrowPointerActive = true;
            }
        }

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
            // Debug.Log("Jump");
            playerAnimator.SetTrigger("JumpUp");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void GravityScale()
    {
        rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
    }

    void GroundCheck()
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
        float speedIncrease = speed * (boostPercentage / 100f);
        speed += speedIncrease;
        Debug.Log("Speed Increased to: " + speed);
        Debug.Log("Speed Increase Percentage: " + boostPercentage);
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
        Debug.Log("Speed Decreased to: " + speed);
        speedIncreaseCoroutine = null;
    }

    public void StartSpeedIncrease(float duration, int boostPercentage)
    {
        if (speedIncreaseCoroutine != null)
        {
            StopCoroutine(speedIncreaseCoroutine);
            speed = defaultSpeed;
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
        normalSpeed = speed;
        twiceSpeed = true;
        speed *= 2;
    }

    public static void DeactivateTwiceSpeed()
    {
        twiceSpeed = false;
        speed = normalSpeed;
    }
}
