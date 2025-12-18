using System;
using System.Collections;
using UnityEngine;

public class ConsoleLight : MonoBehaviour
{
    public Animator animator; // Assign your Animator
    private bool isClickable;
    private Action onClick;

    public void SetStateIdle()
    {
        isClickable = false;
        animator.Play("Idle"); // Ensure idle animation plays
    }

    public void SetStateReady(Action clickAction)
    {
        isClickable = true;
        onClick = clickAction;
        animator.SetTrigger("FlashRed"); // Play your red animation
    }

    public void SetStateSuccess(Action callback = null)
    {
        StartCoroutine(PlayGreenFlash(callback));
    }

    private IEnumerator PlayGreenFlash(Action callback)
    {
        animator.SetTrigger("FlashGreen"); // Play green animation
        yield return new WaitForSeconds(1f); // adjust to animation length
        callback?.Invoke();
    }

    public void OnConsoleClicked()
    {
        if (isClickable && onClick != null)
            onClick.Invoke();
    }
}