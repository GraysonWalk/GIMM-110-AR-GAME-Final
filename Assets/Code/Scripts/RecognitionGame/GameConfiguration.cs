using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
///     ScriptableObject that defines a game configuration for recognition styled games, including
///     an optional video clip for hologram playback games and a list of expected states for image targets.
/// </summary>
[CreateAssetMenu(fileName = "GameConfiguration", menuName = "RecognitionGame/Game Configuration")]
public class GameConfiguration : ScriptableObject
{
    public string configurationName;
    public VideoClip videoClip; // Optional video clip for hologram playback games
    public List<ObjectState> objectStates = new(); // List of expected object states for image targets

    /// <summary>
    ///     Represents the expected state of an AR target in the game configuration. Currently just
    ///     whether it should be active or not.
    /// </summary>
    [Serializable]
    public class ObjectState
    {
        // Assign a target identifier here (e.g. the AR target's GameObject name or a custom id)
        public ARTargetList targetId;
        public bool requireActive = true;

        /// <summary>
        ///     Checks if the given ARTarget matches the expected state defined in this ObjectState.
        /// </summary>
        /// <param name="targetComponent">Image target to check</param>
        /// <returns>True if the target is part of the configuration's solution</returns>
        public bool Matches(ARTarget targetComponent)
        {
            if (targetComponent == null)
            {
                Debug.LogError($"{nameof(targetComponent)} is null");
                return false;
            }

            if (requireActive && !targetComponent.IsActive)
            {
                return false;
            }

            if (targetId == ARTargetList.None) return false;
            return targetComponent.target == targetId;
        }
    }
}