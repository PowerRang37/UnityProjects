using UnityEngine;

namespace OmniumLessons
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CharacterFactory _characterFactory;
        [SerializeField] private GameData _gameData;
        [SerializeField] private CharacterSpawnController _spawnController;

        private bool _isGameActive = false;
        private float _gameTimeSec = 0f;

        public static GameManager Instance { get; private set; }

        public ScoreManager ScoreManager { get; private set; }

        public CharacterFactory CharacterFactory => _characterFactory;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                Initialize();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Initialize()
        {
            ScoreManager = new ScoreManager();

            // Инициализируем спавнер один раз
            // (важно: он будет вызывать OnCharacterDeathHandler для врагов)
            if (_spawnController != null)
                _spawnController.Initialize(_characterFactory, _gameData, OnCharacterDeathHandler);
        }

        public void StartGame()
        {
            if (_isGameActive)
            {
                Debug.Log("Game is already active");
                return;
            }

            var player = CharacterFactory.CreateCharacter(CharacterType.DefaultPlayer);
            player.transform.position = Vector3.zero;
            player.gameObject.SetActive(true);
            player.LiveComponent.OnCharacterDeath += OnCharacterDeathHandler;

            _gameTimeSec = 0f;
            ScoreManager.StartGame();

            _isGameActive = true;

            // Запускаем спавн врагов
            if (_spawnController != null)
                _spawnController.StartSpawning();
        }

        private void Update()
        {
            if (!_isGameActive)
                return;

            _gameTimeSec += Time.deltaTime;

            if (_gameTimeSec >= _gameData.GameTimeSecondsMax)
            {
                GameVictory();
            }
        }

        private void OnCharacterDeathHandler(Character deathCharacter)
        {
            Debug.LogError("character " + deathCharacter.gameObject.name + " is dead");

            switch (deathCharacter.CharacterType)
            {
                case CharacterType.DefaultPlayer:
                    GameOver();
                    break;

                case CharacterType.DefaultEnemy:
                    ScoreManager.AddScore(deathCharacter.CharacterData.ScoreCost);
                    Debug.LogError("Score = " + ScoreManager.GameScore);
                    break;
            }

            CharacterFactory.ReturnCharacterToPool(deathCharacter);
            deathCharacter.gameObject.SetActive(false);

            deathCharacter.LiveComponent.OnCharacterDeath -= OnCharacterDeathHandler;
        }

        private void GameOver()
        {
            Debug.LogError("GameOver!");
            Debug.LogError("Score = " + ScoreManager.GameScore);
            Debug.LogError("ScoreMax = " + ScoreManager.ScoreMax);

            ScoreManager.CompleteMatch();
            _isGameActive = false;

            if (_spawnController != null)
                _spawnController.StopSpawning();
        }

        private void GameVictory()
        {
            Debug.Log("Game Over! Time's up!");

            ScoreManager.CompleteMatch();
            _isGameActive = false;

            if (_spawnController != null)
                _spawnController.StopSpawning();
        }
    }
}
