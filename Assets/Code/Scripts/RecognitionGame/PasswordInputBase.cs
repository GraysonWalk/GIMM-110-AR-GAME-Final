using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///     Password input base class that checks input against a predefined password and invokes events on correct/incorrect
///     input.
/// </summary>
public abstract class PasswordInputBase : MonoBehaviour, IPasswordInput
{
    [SerializeField] protected string password;

    // Inspector-friendly events
    [SerializeField] private UnityEvent onCorrect;

    [SerializeField] private UnityEvent onIncorrect;

    // Runtime subscribers
    public event Action OnCorrect;
    public event Action OnIncorrect;

    public virtual void CheckInput(string input)
    {
        if (input == password)
        {
            // Notify runtime subscribers and inspector events
            OnCorrect?.Invoke();
            onCorrect?.Invoke();
        }
        else
        {
            OnIncorrect?.Invoke();
            onIncorrect?.Invoke();
        }
    }
}