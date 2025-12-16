using UnityEngine;

public class SwapBehavior : MonoBehaviour
{
    [SerializeField] GameObject[] Index1;
    [SerializeField] GameObject[] Index2;
    [SerializeField] GameObject[] Index3;
    private float masterIndex = 0;

    void Start()
    {
        foreach (GameObject elem in Index1)
        {
            elem.SetActive(true);
        }
    }

    public void Swap()
    {
        masterIndex++;
        if (masterIndex >= 3)
        {
            masterIndex = 0;
        }
        switch (masterIndex)
        {
            case 0:
                foreach (GameObject elem in Index1)
                {
                    elem.SetActive(true);
                }
                foreach (GameObject elem in Index3)
                {
                    elem.SetActive(false);
                }
                break;

            case 1:
                foreach (GameObject elem in Index2)
                {
                    elem.SetActive(true);
                }
                foreach (GameObject elem in Index1)
                {
                    elem.SetActive(false);
                }
                break;

            case 2:
                foreach (GameObject elem in Index3)
                {
                    elem.SetActive(true);
                }
                foreach (GameObject elem in Index2)
                {
                    elem.SetActive(false);
                }
                break;
        }
    }

    public void SpecificSwap()
    {

    }
}
