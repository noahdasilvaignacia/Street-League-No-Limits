
using UnityEngine;

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class StreetPlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float sprintMultiplier = 1.5f;
    public float rotationSpeed = 12f;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) input.y += 1;
            if (Keyboard.current.sKey.isPressed) input.y -= 1;
            if (Keyboard.current.aKey.isPressed) input.x -= 1;
            if (Keyboard.current.dKey.isPressed) input.x += 1;
        }

        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime
            );
        }

        bool sprint = Keyboard.current != null &&
                      Keyboard.current.leftShiftKey.isPressed;

        float speed = moveSpeed * (sprint ? sprintMultiplier : 1f);

        controller.Move(direction * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
