using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour {
    Transform shaft;
	// Use this for initialization
	void Start () {
        shaft = transform.Find("Shaft").transform.Find("BladeShaft");
	}
	
	// Update is called once per frame
	void Update () {
        //   shaft.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        shaft.Rotate(Vector3.forward * Time.deltaTime * 50);
      //  shaft.Rotate(Vector3.up * Time.deltaTime, Space.World);
    }
}
