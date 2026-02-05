using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fillImage; // Image Type = Filled (Horizontal, Origin Left)

    [Header("Smoothing (optional)")]
    [SerializeField] private float smoothSpeed = 10f; // 0 = без плавности

    private float _targetFill = 1f;

    private void Reset()
    {
        // Попытка авто-подхватить Image с этого объекта
        if (fillImage == null)
            fillImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (fillImage == null) return;

        if (smoothSpeed <= 0f)
        {
            fillImage.fillAmount = _targetFill;
            return;
        }

        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, _targetFill, Time.deltaTime * smoothSpeed);
    }

    public void SetHealth(float current, float max)
    {
        if (max <= 0f) max = 1f;
        _targetFill = Mathf.Clamp01(current / max);

        if (fillImage == null) return;

        if (smoothSpeed <= 0f)
            fillImage.fillAmount = _targetFill;
    }
}