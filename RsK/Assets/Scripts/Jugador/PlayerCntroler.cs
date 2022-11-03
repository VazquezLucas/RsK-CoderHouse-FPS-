using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class PlayerCntroler : MonoBehaviour
{
    [Header ("References")]
    public Camera playerCamera;

    [Header ("General")]
    public float gravityScale = -20f;

    [Header ("Movement")]
    public float walkSpeed = 20f;
    public float runSpeed = 30f;

    [Header ("Jump")]
    public float jumpHeight = 1.9f;

    [Header ("Rotation")]
    public float rotationSensibility= 10;

    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;

    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Loock();
        Move();
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }
                       

            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2 * gravityScale);
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Loock()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
    }
}
