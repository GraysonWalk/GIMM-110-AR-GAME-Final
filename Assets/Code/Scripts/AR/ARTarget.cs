using UnityEngine;

public class ARTarget : MonoBehaviour
{
    public bool isActive;

    public void Update()
    {
    }

    public void TargetFound()
    {
        isActive = true;
    }

    public void TargetLost()
    {
    }
}