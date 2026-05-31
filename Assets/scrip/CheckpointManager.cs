using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI checkpointText;
    public TextMeshProUGUI finishText;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI countdownText;
    public GameObject tombolNextLevel;
    public GameObject tombolRestart; // tambah ini

    [Header("Level")]
    public string namaSceneBerikutnya = "level 2";
    public float delayPindahScene = 3f;

    [Header("Batas Waktu")]
    public bool adaBatasWaktu = false;
    public float batasWaktu = 120f;

    [Header("Checkpoint")]
    public Checkpoint[] semuaCheckpoint;

    private float timer = 0f;
    private int totalCheckpoints = 0;
    private int passedCheckpoints = 0;
    private bool isFinished = false;
    public bool timerBerjalan = false;
    private Rigidbody mobilRb; // untuk hentikan mobil

    void Start()
    {
        totalCheckpoints = semuaCheckpoint.Length;
        if (finishText) finishText.gameObject.SetActive(false);
        if (timerText) timerText.text = "Waktu: 00:00:00";
        if (checkpointText)
            checkpointText.text = "Checkpoint: 0/" + totalCheckpoints;
        if (startText) startText.gameObject.SetActive(true);
        if (countdownText) countdownText.gameObject.SetActive(false);
        if (tombolNextLevel) tombolNextLevel.SetActive(false);
        if (tombolRestart) tombolRestart.SetActive(false);

        // Cari Rigidbody mobil
        GameObject mobil = GameObject.FindGameObjectWithTag("Player");
        if (mobil != null)
            mobilRb = mobil.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!timerBerjalan || isFinished) return;

        timer += Time.deltaTime;

        int menit = (int)(timer / 60);
        int detik = (int)(timer % 60);
        int milidetik = (int)((timer * 100) % 100);

        if (timerText)
            timerText.text = string.Format("Waktu: {0:00}:{1:00}:{2:00}",
                             menit, detik, milidetik);

        if (adaBatasWaktu)
        {
            float sisaWaktu = batasWaktu - timer;

            if (countdownText)
            {
                countdownText.gameObject.SetActive(true);
                int sisMenit = (int)(sisaWaktu / 60);
                int sisDetik = (int)(sisaWaktu % 60);
                countdownText.text = string.Format("Sisa: {0:00}:{1:00}",
                                     sisMenit, sisDetik);
                countdownText.color = sisaWaktu <= 30f ? Color.red : Color.white;
            }

            if (sisaWaktu <= 0)
            {
                WaktuHabis();
            }
        }
    }

    void WaktuHabis()
    {
        isFinished = true;
        timerBerjalan = false;

        // Hentikan mobil
        if (mobilRb != null)
        {
            mobilRb.linearVelocity = Vector3.zero;
            mobilRb.angularVelocity = Vector3.zero;
            mobilRb.isKinematic = true;
        }

        // Tampilkan teks waktu habis
        if (finishText)
        {
            finishText.gameObject.SetActive(true);
            finishText.color = Color.red;
            finishText.text = "WAKTU HABIS!\nCoba Lagi?";
        }

        // Tampilkan tombol restart
        if (tombolRestart)
            tombolRestart.SetActive(true);

        Debug.Log("Waktu habis! Mobil berhenti.");
    }

    public void MulaiTimer()
    {
        if (timerBerjalan) return;
        timerBerjalan = true;
        timer = 0f;
        if (startText) startText.gameObject.SetActive(false);
        if (countdownText && adaBatasWaktu)
            countdownText.gameObject.SetActive(true);
        Debug.Log("Timer mulai!");
    }

    public void CheckpointPassed()
    {
        if (!timerBerjalan || isFinished) return;
        passedCheckpoints++;
        if (checkpointText)
            checkpointText.text = "Checkpoint: " + passedCheckpoints
                                + "/" + totalCheckpoints;
    }

    public void Finish()
    {
        if (isFinished) return;
        isFinished = true;
        timerBerjalan = false;

        int menit = (int)(timer / 60);
        int detik = (int)(timer % 60);
        int milidetik = (int)((timer * 100) % 100);

        if (finishText)
        {
            finishText.gameObject.SetActive(true);
            finishText.color = Color.green;
            finishText.text = "FINISH!\nWaktu: "
                             + string.Format("{0:00}:{1:00}:{2:00}",
                               menit, detik, milidetik);
        }

        if (tombolNextLevel)
            tombolNextLevel.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(namaSceneBerikutnya);
    }

    // Dipanggil tombol Restart
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PindahLevel()
    {
        SceneManager.LoadScene(namaSceneBerikutnya);
    }
}