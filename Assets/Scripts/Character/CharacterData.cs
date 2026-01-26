using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmniumLessons
{
    public class CharacterData : MonoBehaviour
    {
        // Добавляем информацию о стоимости нашего персонажа за его убийство
        [SerializeField] private int _scoreCost;
        [SerializeField] private float _defaultSpeed;
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private CharacterController _characterController;

        // Ниже представлены свойство. Они похожи на переменные по своей сути.
        // Они управляют доступом к данным через методы get и set.
        // Также могут выполнять дополнительные действия при изменении или запросе данных.
        
        // Полная форма написания свойства
        public float DefaultSpeed
        {
            get { return _defaultSpeed; }
        }
        
        // Сокращенная форма написания свойства
        public CharacterController CharacterController => _characterController;
        
        public Transform CharacterTransform => _characterTransform;
        public int ScoreCost => _scoreCost;
    }
}