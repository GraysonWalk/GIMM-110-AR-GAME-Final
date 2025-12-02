using UnityEngine;

public class SelectionBehavior : MonoBehaviour
{
    [SerializeField] private GridNodeDirectional parentNode;
    private bool notPicked = false;

    public void NotSelected()
    {
        notPicked = true;
    }

    public void Selected()
    {
        if (!notPicked)
        {
            parentNode.onActivate.Invoke();
        }
    }
}
