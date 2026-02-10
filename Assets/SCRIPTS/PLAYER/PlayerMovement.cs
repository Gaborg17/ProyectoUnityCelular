using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveVector;

    public float walkSpeed;
    public float actualSpeed;

    public float jumpForce;
    public bool canJump = true;
    public bool lookingLeft = false;
    [SerializeField] private Transform playerCenter;

    public int direction = 1;

    private Rigidbody rb;
    private CheckGround checkGround;
    private CheckVisibility checkVisibility;

    [SerializeField] float joystickThreshold = 0.9f;

    private bool tryJump;
    private bool wasAboveThreshold;

    [SerializeField]private float coyoteTime = 0.12f;
    private float coyoteTimeCounter;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        checkGround = GetComponent<CheckGround>();
        checkVisibility = GetComponent<CheckVisibility>();
        actualSpeed = walkSpeed;
    }


    public void InputPlayer(InputAction.CallbackContext _context)
    {
        moveVector = _context.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector3(moveVector.x * actualSpeed, rb.linearVelocity.y, 0);
    }

    private void Jump()
    {
        if (checkGround.IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }

        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        bool isAboveThreshold = moveVector.y > joystickThreshold;

        if (isAboveThreshold && !wasAboveThreshold)
        {
            tryJump = true;
        }

        wasAboveThreshold = isAboveThreshold;

        if (tryJump == true && coyoteTimeCounter > 0f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = true;
            tryJump = false;
        }

        

        if(moveVector.y > 0.98f && !checkGround.IsGrounded() && canJump == true)
        {
            rb.AddForce(Vector3.up * jumpForce * 1.3f, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void FlipX()
    {

        if (lookingLeft)
        {
            playerCenter.rotation = Quaternion.Euler(0, 180, 0);
            direction = -1;
        }
        else
        {
            playerCenter.rotation = Quaternion.Euler(0, 0, 0);
            direction = 1;
        }

        if (moveVector.x == 0)
        {
            return;
        }
        else if(moveVector.x > 0.1)
        {
            lookingLeft = false;

        }

        else if (moveVector.x < -0.1)
        {
            lookingLeft = true;
            

        }

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        FlipX();
        OutOfRangeCheck();
    }

    private void OutOfRangeCheck()
    {
        if (!checkVisibility.IsVisible())
        {
            GameManager.Instance.gameOver = true;
            Debug.Log("Fuera de Rango");
        }

    }
}
