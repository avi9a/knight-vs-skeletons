using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos;
    private float length;
    public float parallaxSpeed;
    public new GameObject camera;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
        float temp = camera.transform.position.x * (1 - parallaxSpeed);
        float distans = camera.transform.position.x * parallaxSpeed;
        transform.position = new Vector3(startPos + distans, transform.position.y, transform.position.z);
        if (temp > startPos + length) {
            startPos += length;
        }
        else if (temp < startPos - length) {
            startPos -= length;
        }
    }
}
