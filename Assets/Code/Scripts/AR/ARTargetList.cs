using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AR/ARTargetList")]
public class ARTargetList : ScriptableObject
{
    public List<ARTarget> Targets = new();
}