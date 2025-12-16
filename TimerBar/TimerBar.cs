using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float totalTime = 30f;
    public float TimeLeft => timeLeft;
    public bool active = true;

    [Header("Shake & Flash Settings")]
    [SerializeField] private RectTransform shakeTarget;
    [SerializeField] private float halfShakeIntensity = 5f;
    [SerializeField] private float lowShakeIntensity = 12f;
    [SerializeField] private float shakeSpeed = 50f;
    [SerializeField] private float halfFlashSpeed = 1f;
    [SerializeField] private float lowFlashSpeed = 0.3f;

    [Header("Fill Reference")]
    [SerializeField] private Image fillBar;

    [Header("GameManager Reference")]
    [SerializeField] private GameManager manager;

    private float timeLeft;
    private Coroutine flashRoutine;
    private Coroutine shakeRoutine;
    private bool halfFlashing = false;
    private bool lowFlashing = false;

    private void OnEnable() => StartTimer();

    public void StartTimer()
    {
        timeLeft = totalTime;
        fillBar.fillAmount = 1f;

        if (flashRoutine != null) StopCoroutine(flashRoutine);
        if (shakeRoutine != null) StopCoroutine(shakeRoutine);

        halfFlashing = false;
        lowFlashing = false;
    }

    private void Update()
    {
        if (!active) return;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            fillBar.fillAmount = 0f;

            if (manager != null)
                manager.SwitchState(new FailState(manager));

            return;
        }

        timeLeft -= Time.deltaTime;
        fillBar.fillAmount = timeLeft / totalTime;

        HandleFlashingLogic();
    }

    private void HandleFlashingLogic()
    {
        float percent = timeLeft / totalTime;

        if (percent <= 0.25f && !lowFlashing)
        {
            StartFlashAndShake(lowFlashSpeed, lowShakeIntensity);
            lowFlashing = true;
            return;
        }

        if (percent <= 0.5f && !halfFlashing && !lowFlashing)
        {
            StartFlashAndShake(halfFlashSpeed, halfShakeIntensity);
            halfFlashing = true;
        }
    }

    private void StartFlashAndShake(float flashSpeed, float shakeIntensity)
    {
        if (flashRoutine != null) StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(FlashRoutine(flashSpeed));

        if (shakeRoutine != null) StopCoroutine(shakeRoutine);
        shakeRoutine = StartCoroutine(ShakeRoutine(shakeIntensity));
    }

    private System.Collections.IEnumerator FlashRoutine(float speed)
    {
        while (true)
        {
            Color c = fillBar.color;
            for (float t = 0; t < speed; t += Time.deltaTime)
            {
                c.a = Mathf.Lerp(1f, 0.2f, t / speed);
                fillBar.color = c;
                yield return null;
            }
            for (float t = 0; t < speed; t += Time.deltaTime)
            {
                c.a = Mathf.Lerp(0.2f, 1f, t / speed);
                fillBar.color = c;
                yield return null;
            }
        }
    }

    private System.Collections.IEnumerator ShakeRoutine(float intensity)
    {
        Vector2 originalPos = shakeTarget.anchoredPosition;

        while (true)
        {
            float offsetX = Mathf.Sin(Time.time * shakeSpeed) * intensity;
            float offsetY = Mathf.Cos(Time.time * shakeSpeed * 0.8f) * intensity * 0.5f;

            shakeTarget.anchoredPosition = originalPos + new Vector2(offsetX, offsetY);
            yield return null;
        }
    }
}