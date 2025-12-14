using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     This class will check the inout of a text field against a password and invoke events on correct/incorrect input.
/// </summary>
public class PasswordInput : MonoBehaviour
{
    public string password;
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void CheckInput(string input)
    {
        if (input == password)
        {
            Debug.Log("Password correct");
            _outline.effectColor = Color.green;
        }
        // Invoke correct event
        else
        {
            Debug.Log("Password incorrect");
            _outline.effectColor = Color.red;
        }
        // Invoke incorrect event
    }
}