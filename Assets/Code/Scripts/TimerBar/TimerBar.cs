using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float totalTime = 30f;

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

    private Coroutine flashRoutine;
    private bool halfFlashing;
    private bool lowFlashing;
    private Coroutine shakeRoutine;

    public float TimeLeft { get; private set; }

    private void Update()
    {
        if (!active) return;

        if (TimeLeft <= 0f)
        {
            TimeLeft = 0f;
            fillBar.fillAmount = 0f;

            if (manager != null)
                manager.OnTimeout();

            return;
        }

        TimeLeft -= Time.deltaTime;
        fillBar.fillAmount = TimeLeft / totalTime;

        HandleFlashingLogic();
    }

    private void OnEnable()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        TimeLeft = totalTime;
        fillBar.fillAmount = 1f;

        if (flashRoutine != null) StopCoroutine(flashRoutine);
        if (shakeRoutine != null) StopCoroutine(shakeRoutine);

        halfFlashing = false;
        lowFlashing = false;
    }

    private void HandleFlashingLogic()
    {
        var percent = TimeLeft / totalTime;

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

    private IEnumerator FlashRoutine(float speed)
    {
        while (true)
        {
            var c = fillBar.color;
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

    private IEnumerator ShakeRoutine(float intensity)
    {
        var originalPos = shakeTarget.anchoredPosition;

        while (true)
        {
            var offsetX = Mathf.Sin(Time.time * shakeSpeed) * intensity;
            var offsetY = Mathf.Cos(Time.time * shakeSpeed * 0.8f) * intensity * 0.5f;

            shakeTarget.anchoredPosition = originalPos + new Vector2(offsetX, offsetY);
            yield return null;
        }
    }
}