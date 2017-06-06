using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    Transform target; //target for camera to follow
    public float smoothing = 5f; //little bit of camera lag to smooth out the camera

    Vector3 offset;// distance between camera and player
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset = transform.position - target.position;
       
    }
    // if we used Update() it would be moving in different time to the player
    void FixedUpdate()
    {
        // position of player plus offset
        Vector3 targetCamPos = target.position + offset;
        //Lerp smoothly moves between two positions between current position and targetCamPosition at what speed
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
