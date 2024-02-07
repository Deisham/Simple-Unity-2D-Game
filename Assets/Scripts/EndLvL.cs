using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public GameObject clearMenu;

    void Start()
    {
        ClearMenuSetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone");
            ClearMenuSetActive(true);
            // Замораживаем время
            Time.timeScale = 0f;
        }
    }

    private void ClearMenuSetActive(bool isActive)
    {
        if (clearMenu != null)
        {
            clearMenu.SetActive(isActive);
        }
    }
}
