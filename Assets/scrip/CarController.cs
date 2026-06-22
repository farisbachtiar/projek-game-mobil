using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Pengaturan Mobil")]
    public float moveSpeed = 15f;
    public float turnSpeed = 80f;
    public float brakeForce = 5f;

    [Header("Roda")]
    public Transform wheelFrontLeft;
    public Transform wheelFrontRight;
    public Transform wheelRearLeft;
    public Transform wheelRearRight;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.8f, 0);
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        float keyMove = 0f;
        float keyTurn = 0f;

        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) keyMove = 1f;
            else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) keyMove = -1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) keyTurn = 1f;
            else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) keyTurn = -1f;
        }

        moveInput = keyMove != 0 ? keyMove : AnalogController.Vertical;
        turnInput = keyTurn != 0 ? keyTurn : AnalogController.Horizontal;

        RotateWheels(moveInput);
    }

    void FixedUpdate()
    {
        // Stop semua input saat Game Over
        if (rb.isKinematic) return;

        Vector3 force = transform.forward * moveInput * moveSpeed * 100f;
        rb.AddForce(force, ForceMode.Force);

        if (rb.linearVelocity.magnitude > moveSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;

        if (moveInput == 0f)
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero,
                            brakeForce * Time.fixedDeltaTime);

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        Vector3 euler = rb.rotation.eulerAngles;
        rb.rotation = Quaternion.Euler(0f, euler.y, 0f);
    }

    void RotateWheels(float direction)
    {
        float rotationAmount = direction * 300f * Time.deltaTime;
        if (wheelFrontLeft) wheelFrontLeft.Rotate(rotationAmount, 0, 0);
        if (wheelFrontRight) wheelFrontRight.Rotate(rotationAmount, 0, 0);
        if (wheelRearLeft) wheelRearLeft.Rotate(rotationAmount, 0, 0);
        if (wheelRearRight) wheelRearRight.Rotate(rotationAmount, 0, 0);
    }
}