using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // This is the gameobject the camera will follow
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        // This is the position of the camera and it being change to the targets position
        transform.position = new Vector3(target.transform.position.x, 26, target.transform.position.z - 10);
    }
}
