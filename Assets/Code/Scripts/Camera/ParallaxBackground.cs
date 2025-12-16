using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{
    private float length;
    private float startPos;
    private float yHeight;
    [SerializeField] public GameObject cam;
    [SerializeField] public float parallaxFactor;
    
    void Start()
    {
        yHeight = transform.position.y;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void Update()
    {
        float temp = cam.transform.position.x * (1-parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;

        transform.position = new Vector3(startPos + distance, yHeight, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
