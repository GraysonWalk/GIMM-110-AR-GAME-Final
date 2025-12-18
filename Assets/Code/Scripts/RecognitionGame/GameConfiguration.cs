using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "RecognitionGame/Game Configuration")]
public class GameConfiguration : ScriptableObject
{
    public string configurationName;
    public VideoClip videoClip;
    public string password;
    public List<ObjectState> objectStates = new();

    [Serializable]
    public class ObjectState
    {
        // Reference to the required AR target definition asset
        public ARTargetDefinition targetDefinition;
        public bool requireActive = true;

        public bool Matches(ARTarget targetComponent)
        {
            if (targetComponent == null)
            {
                Debug.LogError($"{nameof(targetComponent)} is null");
                return false;
            }

            if (requireActive && !targetComponent.IsActive) return false;

            if (targetDefinition == null) return false;
            return targetComponent.Definition != null && targetComponent.Definition.Id == targetDefinition.Id;
        }
    }
}