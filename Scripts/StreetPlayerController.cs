using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class StreetPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintMultiplier = 1.5f;
    public float rotationSpeed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 1.2f;

    CharacterController controller;
    Vector2 mobileMove;
    bool mobileSprint;
    bool jumpQueued;
    Vector3 verticalVelocity;

    void Awake() => controller = GetComponent<CharacterController>();

    void Update()
    {
        // Keyboard on PC; mobile UI can call SetMobileMove/SetSprint.
        Vector2 input = mobileMove;
        if (input.sqrMagnitude < 0.001f)
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 direction = new Vector3(input.x, 0f, input.y);
        direction = Vector3.ClampMagnitude(direction, 1f);

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }

        bool sprint = mobileSprint || Input.GetKey(KeyCode.LeftShift);
        float speed = moveSpeed * (sprint ? sprintMultiplier : 1f);
        controller.Move(direction * speed * Time.deltaTime);

        if (controller.isGrounded && verticalVelocity.y < 0f)
            verticalVelocity.y = -2f;

        if ((Input.GetKeyDown(KeyCode.Space) || jumpQueued) && controller.isGrounded)
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        jumpQueued = false;
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    // Hook these methods to on-screen mobile controls.
    public void SetMobileMove(Vector2 value) => mobileMove = Vector2.ClampMagnitude(value, 1f);
    public void SetSprint(bool value) => mobileSprint = value;
    public void Jump() => jumpQueued = true;
}
