using System;
using System.Collections.Generic;
using UnityEngine;

namespace OmniumLessons
{
    public class CharacterSpawnController : MonoBehaviour
    {
        [SerializeField] private int maxEnemies = 10;

        private Action<Character> _onCharacterDeathCallback;
        private CharacterFactory _factory;
        private GameData _gameData;

        private float _timer;
        private readonly List<Character> _aliveEnemies = new List<Character>();
        private bool _isActive;

        public void Initialize(CharacterFactory factory, GameData gameData, Action<Character> onCharacterDeathCallback)
        {
            _factory = factory;
            _gameData = gameData;
            _onCharacterDeathCallback = onCharacterDeathCallback;
        }

        public void StartSpawning()
        {
            _timer = 0f;
            _isActive = true;
        }

        public void StopSpawning()
        {
            _isActive = false;
        }

        private void Update()
        {
            if (!_isActive) return;
            if (_factory == null || _gameData == null) return;
            if (_factory.Player == null) return;

            if (_aliveEnemies.Count >= maxEnemies)
                return;

            _timer += Time.deltaTime;
            if (_timer >= _gameData.TimeBetweenEnemySpawn)
            {
                SpawnEnemy();
                _timer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            var enemy = _factory.CreateCharacter(CharacterType.DefaultEnemy);

            float posX = _factory.Player.transform.position.x + GetOffset();
            float posZ = _factory.Player.transform.position.z + GetOffset();
            enemy.transform.position = new Vector3(posX, 0f, posZ);

            // 1) GameManager начислит очки и вернёт в пул
            enemy.LiveComponent.OnCharacterDeath += _onCharacterDeathCallback;

            // 2) SpawnController уменьшит счётчик живых врагов
            enemy.LiveComponent.OnCharacterDeath += OnEnemyDeath;

            enemy.gameObject.SetActive(true);
            _aliveEnemies.Add(enemy);

            float GetOffset()
            {
                bool isPlus = UnityEngine.Random.Range(0, 2) > 0;
                float randomOffset = UnityEngine.Random.Range(_gameData.MinEnemySpawnOffset, _gameData.MaxEnemySpawnOffset);
                return isPlus ? randomOffset : -randomOffset;
            }
        }

        private void OnEnemyDeath(Character deadCharacter)
        {
            // Снимаем нашу подписку, чтобы не было накопления
            deadCharacter.LiveComponent.OnCharacterDeath -= OnEnemyDeath;

            _aliveEnemies.Remove(deadCharacter);
        }
    }
}
