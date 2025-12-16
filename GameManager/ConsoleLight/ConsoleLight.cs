using UnityEngine;
using System.Collections;

public class ConsoleLight : MonoBehaviour
{
    public Animator animator;
    private bool isClickable = false;
    private System.Action onClick;
    private Coroutine currentFlashCoroutine;

    public void SetStateIdle()
    {
        isClickable = false;
        onClick = null;
        animator?.Play("Idle");
    }

    public void SetStateReady(System.Action clickAction)
    {
        isClickable = true;
        onClick = clickAction;
        animator?.SetTrigger("FlashRed");
    }

    public void SetStateSuccess(System.Action callback = null)
    {
        if (currentFlashCoroutine != null) StopCoroutine(currentFlashCoroutine);
        currentFlashCoroutine = StartCoroutine(PlayGreenFlash(callback));
    }

    private IEnumerator PlayGreenFlash(System.Action callback)
    {
        animator?.SetTrigger("FlashGreen");
        yield return new WaitForSeconds(1f);
        callback?.Invoke();
        currentFlashCoroutine = null;
    }

    public void OnConsoleClicked()
    {
        if (isClickable && onClick != null) onClick.Invoke();
    }
}