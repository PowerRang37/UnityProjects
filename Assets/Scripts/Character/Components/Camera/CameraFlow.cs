using UnityEngine;
using OmniumLessons;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -10f);
    [SerializeField] private float smoothTime = 0.12f;

    private Vector3 velocity;
    private Quaternion fixedRotation;

    private void Awake()
    {
        // Запоминаем текущий поворот камеры и больше его не меняем
        fixedRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        // Если target не назначен — пробуем найти игрока из учительской архитектуры
        if (target == null && GameManager.Instance != null && GameManager.Instance.CharacterFactory != null)
        {
            var player = GameManager.Instance.CharacterFactory.Player;
            if (player != null)
                target = player.transform;
        }

        if (target == null)
            return;

        // Плавно следуем за игроком
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Жёстко фиксируем поворот (камера не крутится)
        transform.rotation = fixedRotation;
    }
}
