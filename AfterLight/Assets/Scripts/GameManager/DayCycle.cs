using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {
     float sunRotateSpeed = 0f;
    GameObject sun;
    Transform sunTransform;
    float sunAngle;
    bool isDay = true;
    bool isNight;
    private void Awake()
    {
        sun = GameObject.Find("Sun");
        sunTransform = sun.GetComponent<Transform>();
    }

    void Update () {
       sunTransform.RotateAround(Vector3.zero, Vector3.right, sunRotateSpeed * Time.deltaTime);
        sunTransform.LookAt(Vector3.zero);
         sunAngle = sunTransform.rotation.eulerAngles.x;
        //print(sunTransform.localRotation);
        // print(sunTransform.rotation.eulerAngles.x);
        //print(sunAngle);
        // print("isDay: " + isDay + " " + sunAngle);

        if (sunAngle < 350 && sunAngle > 200 && !isNight)
        {
            isNight = true;
            isDay = false;
            print("its night");
        }
        if (sunAngle > 10 && sunAngle < 90 && !isDay)
        {
            isDay = true;
            isNight = false;
            print("its Day");
        }

        // StartCoroutine(DetermineTimeOfDay());

    }
    void ChangeCycle()
    {
        //if (!isCurrentlyDark)
        //{
        //    isCurrentlyLight = false;
        //    isCurrentlyDark = true;
        //    print("dark");
        //}
        //if (!isCurrentlyLight)
        //{
        //    isCurrentlyDark = false;
        //    isCurrentlyLight = true;
        //    print("light");
        //}

    }
    IEnumerator DetermineTimeOfDay()
    {
        float a = sunAngle;
        yield return new WaitForEndOfFrame();
        float b = sunAngle;
        if(a > b)
        {
            isDay = true;
            isNight = false;
        }
        else
        {
            isNight = true;
            isDay = false;
        }

    }
    public bool GetIsDay()
    {
        return isDay;
    }
    public bool GetIsNight()
    {
        return isNight;
    }
}
