using UnityEngine;

public class ParallaxLoop : MonoBehaviour
{
    public float parallaxFactor;
    private Transform cam;
    private float length, startPos;

    void Start()
    {
        cam = Camera.main.transform;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = cam.position.x * (1 - parallaxFactor);
        float dist = cam.position.x * parallaxFactor;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
