using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Pengaturan Mobil")]
    public float kecepatanMaju = 15f;
    public float kecepatanBelok = 80f;
    public float kecepatanRem = 5f;

    private Rigidbody rb;
    private float inputMaju;
    private float inputBelok;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Bikin smooth
    }

    void Update()
    {
        // Baca input keyboard
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        inputMaju = 0f;
        inputBelok = 0f;

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) inputMaju = 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) inputMaju = -1f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) inputBelok = -1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) inputBelok = 1f;
    }

    void FixedUpdate()
    {
        // === GERAK MAJU/MUNDUR (smooth) ===
        Vector3 targetVelocity = transform.forward * inputMaju * kecepatanMaju;
        targetVelocity.y = rb.linearVelocity.y; // Jaga gravitasi tetap bekerja
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * kecepatanRem);

        // === BELOK (hanya saat bergerak) ===
        if (inputMaju != 0f)
        {
            float belok = inputBelok * kecepatanBelok * Time.fixedDeltaTime;
            Quaternion putaran = Quaternion.Euler(0f, belok, 0f);
            rb.MoveRotation(rb.rotation * putaran);
        }
    }
}