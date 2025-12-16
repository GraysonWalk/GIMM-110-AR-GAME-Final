using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Recognition game password input that changes outline color based on correctness of input.
/// </summary>
public class RecGamePasswordInput : PasswordInputBase
{
    [SerializeField] private Outline outline;
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        if (outline == null)
            outline = GetComponent<Outline>();
    }

    public void Start()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }

    public override void CheckInput(string input)
    {
        base.CheckInput(input);

        if (input == password)
        {
            Debug.Log("Password correct");
            if (outline != null) outline.effectColor = Color.paleGreen;
        }
        else
        {
            Debug.Log("Password incorrect");
            if (outline != null) outline.effectColor = Color.indianRed;
        }
    }
}