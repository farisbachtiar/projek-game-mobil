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
        Debug.Log("Ada yang masuk trigger FINISH: " + other.name);

        if (sudahDilewati) return;

        bool isPlayer = other.CompareTag("Player")
                     || other.transform.root.CompareTag("Player")
                     || other.name.Contains("mobil")
                     || other.name.Contains("Car")
                     || other.name.Contains("Wheel");

        if (isPlayer)
        {
            sudahDilewati = true;
            if (manager != null) manager.Finish();
            Debug.Log("FINISH dilewati oleh: " + other.name);
        }
    }
}