using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject helpMenu;

    private void Start()
    {
        // Скрываем окно при запуске сцены
        helpMenu.SetActive(false);
    }
    private void Update()
    {
        // Отслеживаем нажатие клавиши "`"
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Если меню открыто, то закрываем, иначе открываем
            if (helpMenu.gameObject.activeSelf)
                HideHelpMenu();
            else
                ShowHelpMenu();
        }
    }
    
    public void ShowHelpMenu()
    {
        // Показываем окно при вызове этой функции
        helpMenu.SetActive(true);
    }

    public void HideHelpMenu()
    {
        // Скрываем окно при вызове этой функции
        helpMenu.SetActive(false);
    }
}
