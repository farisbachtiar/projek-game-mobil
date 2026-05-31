using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int nomorCheckpoint = 0;
    private bool sudahDilewati = false;
    private CheckpointManager manager;

    void Start()
    {
        manager = FindObjectOfType<CheckpointManager>();
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (sudahDilewati) return;

        bool isPlayer = other.CompareTag("Player")
                     || other.transform.root.CompareTag("Player")
                     || other.name.Contains("mobil")
                     || other.name.Contains("Car")
                     || other.name.Contains("Wheel");

        if (isPlayer)
        {
            sudahDilewati = true;
            if (manager != null) manager.CheckpointPassed();

            if (GetComponent<Renderer>())
                GetComponent<Renderer>().material.color = Color.green;

            Debug.Log("Checkpoint " + nomorCheckpoint + " dilewati!");
        }
    }
}