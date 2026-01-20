using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveVector;

    public float walkSpeed;
    public float jumpForce;
    public bool canJump = true;

    private Rigidbody rb;
    private CheckGround checkGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        checkGround = GetComponent<CheckGround>();
    }


    public void InputPlayer(InputAction.CallbackContext _context)
    {
        moveVector = _context.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector3(moveVector.x * walkSpeed, rb.linearVelocity.y, 0);
    }

    private void Jump()
    {
        if(moveVector.y > 0.65f && checkGround.IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }


    private void OnBecameInvisible()
    {
        Debug.Log("Fuera de encuadre");
    }
}
