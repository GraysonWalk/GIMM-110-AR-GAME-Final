using UnityEngine;
using UnityEngine.Events;

public class GridNodeDirectional : MonoBehaviour
{
    // Class for movement Node element storage

    // Contains assigned nodes to progress to/connect
    [Header("Directional Links")]
    [SerializeField] public GridNodeDirectional up;
    [SerializeField] public GridNodeDirectional down;
    [SerializeField] public GridNodeDirectional left;
    [SerializeField] public GridNodeDirectional right;

    // Contains Unity Events and Bool for player interaction
    [Header("Interaction")]
    [SerializeField] public bool canInteract;
    [SerializeField] public UnityEvent onActivate;
    [SerializeField] public UnityEvent onDeactivate;


}
