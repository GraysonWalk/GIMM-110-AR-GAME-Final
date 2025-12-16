using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TypewriterEffect : MonoBehaviour
{
    [FormerlySerializedAs("m_TextMesh")] [SerializeField]
    private TextMeshProUGUI textMesh;

    [Tooltip("Characters written per second")]
    [SerializeField]
    private float charSpeed = 2f;

    private string _message; //The string we will show to the player with this typewriter effect

    private Coroutine _typewriterCoroutine;

    public bool IsRunning { get; private set; }

    public float TimeInterval =>
        1f / charSpeed; //Calculated each time we need to get this value, in case we change effect speed while this effect is still running

    private void Awake()
    {
        textMesh.text = ""; //Make sure the default content of text mesh won't be seen, even for a glimpse of a frame
    }

    private void Update()
    {
        if (IsRunning)
        {
            var isPlayerSkipping = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
            if (isPlayerSkipping) StopTypewriterEffect();
        }
    }

    public void StartTypewriterEffect(string message)
    {
        _message = message;
        IsRunning = true;
        StopTypewriterEffect(); //Stop any previous typewriter effect that could be running
        _typewriterCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        textMesh.text = message;
        textMesh.maxVisibleCharacters = 0;
        var totalCharacters = message.Length;
        WaitForSeconds wait = new(TimeInterval);

        while (textMesh.maxVisibleCharacters < totalCharacters)
        {
            textMesh.maxVisibleCharacters++;
            yield return wait;
        }

        IsRunning = false;
        print("Typewriter effect finished");
    }

    public void StopTypewriterEffect()
    {
        if (_typewriterCoroutine != null) StopCoroutine(_typewriterCoroutine);
        textMesh.text = _message;
        textMesh.maxVisibleCharacters = int.MaxValue;
        print("Typewriter effect stopped");
        IsRunning = false;
    }
}

/*
 * Adapted from https://github.com/Gord10/UnityTypewriterEffect/blob/main/Assets/Scripts/TypewriterEffect.cs
 */