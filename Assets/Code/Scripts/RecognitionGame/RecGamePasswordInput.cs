using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Recognition game password input that changes outline color based on correctness of input.
/// </summary>
public class RecGamePasswordInput : PasswordInputBase
{
    [SerializeField] private Outline outline;

    private void Awake()
    {
        if (outline == null)
            outline = GetComponent<Outline>();
    }

    public override void CheckInput(string input)
    {
        base.CheckInput(input);

        if (input == password)
        {
            Debug.Log("Password correct");
            if (outline != null) outline.effectColor = Color.green;
        }
        else
        {
            Debug.Log("Password incorrect");
            if (outline != null) outline.effectColor = Color.red;
        }
    }
}