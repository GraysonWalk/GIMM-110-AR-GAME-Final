using UnityEngine;

public class SelectionBehavior : MonoBehaviour
{
    [SerializeField] private GridNodeDirectional parentNode;
    private bool notPicked = false;

    // Boolean check/setter
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
