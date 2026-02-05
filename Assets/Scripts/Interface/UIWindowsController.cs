using UnityEngine;

namespace OmniumLessons
{
    public class UIWindowsController : MonoBehaviour
    {
        [Header("Windows")]
        [SerializeField] private GameObject mainMenuWindow;
        [SerializeField] private GameObject hudWindow;
        [SerializeField] private GameObject pauseWindow;
        [SerializeField] private GameObject upgradesWindow; // добавим сейчас, даже если пустой

        private void Start()
        {
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            Time.timeScale = 1f;
            mainMenuWindow.SetActive(true);
            hudWindow.SetActive(false);
            pauseWindow.SetActive(false);
            if (upgradesWindow != null) upgradesWindow.SetActive(false);
        }

        public void StartGame()
        {
            Time.timeScale = 1f;
            mainMenuWindow.SetActive(false);
            hudWindow.SetActive(true);
            pauseWindow.SetActive(false);
            if (upgradesWindow != null) upgradesWindow.SetActive(false);

            // Тут позже подключю реальный старт игры
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            pauseWindow.SetActive(true);
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            pauseWindow.SetActive(false);
        }

        public void OpenUpgrades()
        {
            if (upgradesWindow == null) return;
            upgradesWindow.SetActive(true);
        }

        public void CloseUpgrades()
        {
            if (upgradesWindow == null) return;
            upgradesWindow.SetActive(false);
        }
    }
}