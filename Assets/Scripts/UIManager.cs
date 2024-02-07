using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject deathPanel;
    public Text deathMessage;

    void Start()
    {
        deathPanel.SetActive(false);
    }

    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
        deathMessage.text = "You Died!";
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
