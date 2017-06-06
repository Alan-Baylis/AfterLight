using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoweredLight : MonoBehaviour {
    PlayerManager playerManager;

    float powerDraw = 1;
    PowerNode thisNode;
    Consumer thisConsumer;
    bool isLightOn;// is the light already on
  //  bool isLightPowered;// does the light have power

    Light thisLight;
    LayerMask enemyLayer;
    CapsuleCollider lightPlaneCollider;

    GameObject lightCanvas;
    Image onLightImage;
    Image offLightImage;
    Color onLightColor = Color.green;
    Color offLightColor = Color.red;
    Color inActiveColor = Color.black;

    // Use this for initialization
    void Start() {
        playerManager = GameManager.Player;
        thisNode = GetComponentInParent<PowerNode>();
        thisConsumer = GetComponentInParent<Consumer>();

        thisLight = transform.Find("Light").GetComponent<Light>();
        enemyLayer = LayerMask.GetMask("Enemy");
        lightPlaneCollider = transform.Find("LightPlane").GetComponent<CapsuleCollider>();
        lightCanvas = transform.Find("LightCanvas").gameObject;
        onLightImage = lightCanvas.transform.Find("OnLightImage").GetComponent<Image>();
        offLightImage = lightCanvas.transform.Find("OffLightImage").GetComponent<Image>();
        offLightImage.color = offLightColor;
        onLightImage.color = inActiveColor;
    }

    // Update is called once per frame
    void Update() {
        CheckLightSwitch();
    }


    void CheckLightSwitch()
    {
        if (Input.GetButtonUp("Submit") && thisConsumer.GetIsPlayerNear() && 
            !playerManager.GetIsInteractionButtonLocked() && !playerManager.GetIsHoldingWireTool()) 
        {
            if (!isLightOn && thisConsumer.GetIsPowered())
            {
                print("turn light on");
                TurnLightOn();
            }
            else if (isLightOn)
            {
                print("turn light off");
                TurnLightOff();
            }
        }
        if (!thisConsumer.GetIsPowered()) TurnLightOff();
    }

    public void TurnLightOff()
    {
        thisLight.enabled = false;
        offLightImage.color = offLightColor;
        onLightImage.color = inActiveColor;
        isLightOn = false;
        lightPlaneCollider.enabled = false;
        //no light no power draw
        thisConsumer.SetPowerDraw(0); 
       // if (powerGrid != null) powerGrid.CalculatePowerDraw();

    }
    public void TurnLightOn()
    {
        isLightOn = true;
        onLightImage.color = onLightColor;
        offLightImage.color = inActiveColor;
        thisLight.enabled = true;
        lightPlaneCollider.enabled = true;
        // power grid gets the power draw from the consumer which in turn gets it from the light
        thisConsumer.SetPowerDraw(powerDraw); 
      //  powerGrid.CalculatePowerDraw();

    }
}
