using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision dengan: " + collision.gameObject.name);

        Rigidbody rb = collision.transform.root.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity *= 0.3f;
            Debug.Log("Mobil melambat! Kecepatan: " + rb.linearVelocity.magnitude);
        }
    }
}