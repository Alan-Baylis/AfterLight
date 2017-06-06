using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnTriggers : MonoBehaviour
{
    BoxCollider boxCollider;


    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("IPower"))
        {
            other.GetComponent<IPower>().SetIsPlayerNear(true);
        }
        else if (other.gameObject.tag.Equals("Line"))
        {
            other.GetComponentInParent<Line>().SetIsPlayerNear(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("IPower"))
        {
            other.GetComponent<IPower>().SetIsPlayerNear(false);
        }
        else if (other.gameObject.tag.Equals("Line"))
        {
            other.GetComponentInParent<Line>().SetIsPlayerNear(false);
        }
    }

}
