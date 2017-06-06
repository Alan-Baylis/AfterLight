using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]

public class PlayerShoot : MonoBehaviour {
    LineRenderer gunLine;
    Transform fireTransform;
    int range = 100;
    public float force = 600f;
    public float accuracyY = .03f;
    public float accuracyX = .03f;
    public float accuracyZ = .03f;
    [HideInInspector] public bool isHasMouseControl = true;
    LayerMask shootMask;
	// Use this for initialization
	void Start () {
     
        gunLine = GetComponent<LineRenderer>();       
        fireTransform = GameObject.FindGameObjectWithTag("FireTransform").GetComponent<Transform>();
        shootMask = LayerMask.GetMask("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetMouseButtonDown(0) && isHasMouseControl)
        {
           // Fire();
        }
	}
    void Fire()
    {
        gunLine.enabled = true;
        gunLine.SetPosition(0, fireTransform.position);
        
        Vector3 direction = AccuracyModifier(fireTransform);
        Ray ray = new Ray(fireTransform.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, shootMask))
        {
            gunLine.SetPosition(1, hit.point);
            print(hit.collider.name);            
          //  Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
            //tell the enemy what limb is missing
           // enemy.CheckLimb(hit.collider.name);
          //  enemy.BlowOffLimb(hit.collider, direction, force);
        }
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, ray.origin + ray.direction * range);
        }
    }
    Vector3 AccuracyModifier(Transform transform)
    {
        Vector3 direction = transform.forward;
        direction.x += Random.Range(-accuracyX, accuracyX);
        direction.y += Random.Range(-accuracyY, accuracyY);
        direction.z += Random.Range(-accuracyZ, accuracyZ);
        return direction;
    }
}
