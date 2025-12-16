using UnityEngine;

public class TypeTest : MonoBehaviour
{
    [SerializeField] [TextArea] private string message;
    [SerializeField] private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect.StartTypewriterEffect(message);
    }
}