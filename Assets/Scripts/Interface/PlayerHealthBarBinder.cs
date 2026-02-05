using UnityEngine;

namespace OmniumLessons
{
    public class PlayerHealthBarBinder : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private HealthBarUI healthBarUI;

        [Header("Behavior")]
        [SerializeField] private bool showEmptyWhenNoPlayer = true;

        private void Awake()
        {
            if (healthBarUI == null)
                healthBarUI = GetComponent<HealthBarUI>();
        }

        private void Update()
        {
            if (healthBarUI == null)
                return;

            // Ищем игрока через твою текущую архитектуру (CharacterFactory.Player)
            if (GameManager.Instance == null ||
                GameManager.Instance.CharacterFactory == null ||
                GameManager.Instance.CharacterFactory.Player == null ||
                GameManager.Instance.CharacterFactory.Player.LiveComponent == null)
            {
                if (showEmptyWhenNoPlayer)
                    healthBarUI.SetHealth(0f, 1f);

                return;
            }

            var live = GameManager.Instance.CharacterFactory.Player.LiveComponent;

            // live.Health = float, live.MaxHealth = int (по твоему ILiveComponent.cs)
            healthBarUI.SetHealth(live.Health, live.MaxHealth);
        }
    }
}