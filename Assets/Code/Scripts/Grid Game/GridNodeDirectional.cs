using UnityEngine;
using UnityEngine.Events;

public class GridNodeDirectional : MonoBehaviour
{
    [Header("Directional Links")]
    [SerializeField] public GridNodeDirectional up;
    [SerializeField] public GridNodeDirectional down;
    [SerializeField] public GridNodeDirectional left;
    [SerializeField] public GridNodeDirectional right;

    [Header("Interaction")]
    [SerializeField] public bool canInteract;
    [SerializeField] public UnityEvent onActivate;
    [SerializeField] public UnityEvent onDeactivate;


}
