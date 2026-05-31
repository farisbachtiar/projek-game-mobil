using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void MulaiGame()
    {
        SceneManager.LoadScene("level 1");
    }

    public void KeluarGame()
    {
        Application.Quit();
        Debug.Log("Keluar!");
    }
}