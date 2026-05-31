using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Pengaturan Kamera")]
    public float height = 6f;
    public float distance = 12f;
    public float smoothSpeed = 8f;

    void LateUpdate()
    {
        if (target == null) return;

        // Posisi kamera di belakang dan atas mobil
        Vector3 desiredPosition = target.position
            - target.forward * distance
            + Vector3.up * height;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Selalu lihat ke mobil
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}