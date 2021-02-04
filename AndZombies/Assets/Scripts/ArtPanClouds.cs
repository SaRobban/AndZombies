using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPanClouds : MonoBehaviour
{
    public float speed;
    public float maxX = 15;
    public float minX = -15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if(transform.position.x > maxX){
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        if(transform.position.x < minX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
    }
}
