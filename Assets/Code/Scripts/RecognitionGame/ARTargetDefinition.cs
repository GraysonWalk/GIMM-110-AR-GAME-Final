using UnityEngine;

[CreateAssetMenu(fileName = "ARTargetDefinition", menuName = "RecognitionGame/ARTarget Definition")]
public class ARTargetDefinition : ScriptableObject
{
    [SerializeField] private string id = System.Guid.NewGuid().ToString();
    public string Id => id;

    public string displayName;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id)) id = System.Guid.NewGuid().ToString();
    }
#endif
}