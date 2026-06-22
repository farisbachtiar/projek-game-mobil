using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool sudahDilewati = false;
    private CheckpointManager manager;

    void Start()
    {
        manager = FindObjectOfType<CheckpointManager>();
        GetComponent<Collider>().isTrigger = true;
        if (manager == null)
            Debug.LogError("CheckpointManager tidak ditemukan!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (sudahDilewati) return;

        // Hanya deteksi Player saja
        if (!other.CompareTag("Player") && 
            !other.transform.root.CompareTag("Player")) return;

        sudahDilewati = true;
        if (manager != null) manager.Finish();
        Debug.Log("FINISH dilewati oleh: " + other.name);
    }
}