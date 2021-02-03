using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UglyPhysics : MonoBehaviour
{

    public Transform parentObj;
    public Vector2 offsett;

    

    // Update is called once per frame
    void Update()
    {
        transform.position = parentObj.TransformPoint(offsett);
        transform.rotation = parentObj.rotation;
    }
}
