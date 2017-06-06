//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//public class SmallLight : MonoBehaviour, IPoweredObject
//{
//    Battery connectedBattery;
//    LayerMask enemyLayer;
//    Light thisLight;
//    [SerializeField]PowerGrid powerGrid;
//    List<GameObject> linesCurrentlyAttachedTo = new List<GameObject>();
//    CapsuleCollider thisCollider;
//    GridManager gridManager;
//    public float powerDraw = 1;
//    PlayerManager playerManager;
//    bool isConnected;
//    bool isConnecting;
//    bool isPlayerNear;
//    [SerializeField] bool hasPower;
//    [SerializeField] bool isLightOn;
//    string gameObjectName;
//    GameObject lightCanvas;
//    Image onLight;
//    Image offLight;
//    Color onLightColor = Color.green;
//    Color offLightColor = Color.red;
//    Color inActiveColor = Color.black;
//    public bool IsDrawingPower
//    {
//        get { return isLightOn; }
//        set { hasPower = value; }
//    }
//    public bool IsBattery
//    {
//        get { return IsBattery = false; }
//        set { }
//    }
//    public string ObjectName
//    {
//        get { return gameObjectName; }
//        set { }
//    }
//    public bool IsPowered
//    {
//        get { return hasPower; }
//        set { hasPower = value; }
//    }
//    public bool IsPlayerNear
//    {
//        get { return isPlayerNear; }
//        set { isPlayerNear = value; }
//    }

//    private void Start()
//    {
//        thisLight = transform.Find("Light").GetComponent<Light>();
//        gridManager = GameManager.Grid;
//        enemyLayer = LayerMask.GetMask("Enemy");
//        playerManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
//        playerManager = GameManager.Player;
//        thisCollider = transform.Find("LightPlane").GetComponent<CapsuleCollider>();
//        lightCanvas = transform.Find("LightCanvas").gameObject;
//        onLight = lightCanvas.transform.Find("OnLightImage").GetComponent<Image>();
//        offLight = lightCanvas.transform.Find("OffLightImage").GetComponent<Image>();

//        gameObjectName = gameObject.name;
//        offLight.color = offLightColor;
//        onLight.color = inActiveColor;
//        print(offLight.name);
//    }
//    private void Update()
//    {
//        CheckForIncomingLines();
//        if (Input.GetButtonUp("Submit") && isPlayerNear && !playerManager.GetIsInteractionButtonLocked()) // !gridManager.isDrawingLine)
//        {
//            print("light");
//            if (!isLightOn && IsPowered)
//            {
//                TurnOn();
//            }
//            else if (isLightOn)
//            {
//                TurnOff();
//            }
//        }
//        if (!IsPowered) TurnOff();
//    }


//    public float GetPowerDraw()
//    {
//        return powerDraw;
//    }
//    //public void DrawPower(GameObject batteryToDrawFrom)
//    //{
//    //     power.DrawPower(batteryToDrawFrom);
//    //}

//    //void CheckIfPowered()
//    //{
//    //    if (!isPowered && isLightOn)
//    //    {
//    //        TurnOffLight();
//    //    }
//    //    if (isPowered && !isLightOn)
//    //    {
//    //        TurnOnLight();
//    //    }
//    //}
//    public void TurnOff()
//    {
//        thisLight.enabled = false;
//        offLight.color = offLightColor;
//        onLight.color = inActiveColor;
//        isLightOn = false;
//        thisCollider.enabled = false;
//        if (powerGrid != null) powerGrid.CalculatePowerDraw();

//    }
//    public void TurnOn()
//    {
//        isLightOn = true;
//        onLight.color = onLightColor;
//        offLight.color = inActiveColor;
//        thisLight.enabled = true;
//        thisCollider.enabled = true;
//        powerGrid.CalculatePowerDraw();

//    }

//    void CheckForIncomingLines()
//    {
//        if (Input.GetButtonUp("Submit") && isPlayerNear && gridManager.isDrawingLine)
//        {
//            gridManager.EndConnectingLine(gameObject);
//        }
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            isPlayerNear = true;
//        }
//    }
//    private void OnTriggerExit(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            isPlayerNear = false;
//        }
//    }

//    //public PowerGrid GetPowerGridAttachedTo()
//    //{
//    //    return powerGrid;
//    //}

//    //public void AddToLinesCurrentlyAttached(GameObject line)
//    //{
//    //    linesCurrentlyAttachedTo.Add(line);
//    //}

//    //public List<GameObject> GetLinesCurrentlyAttached()
//    //{
//    //    return linesCurrentlyAttachedTo;
//    //}

//    //public void SetPowerGridAttachedTo(PowerGrid powerGrid)
//    //{
//    //    this.powerGrid = powerGrid;
//    //}

//    //public void ClearNullsOutOfLineList()
//    //{
//    //    List<GameObject> temp = new List<GameObject>();
//    //   foreach(var obj in linesCurrentlyAttachedTo)
//    //    {
//    //        if(obj != null)
//    //        {
//    //            temp.Add(obj);
//    //        }
//    //    }
//    //    linesCurrentlyAttachedTo.Clear();
//    //    foreach (var obj in temp)
//    //    {
//    //        if (obj != null)
//    //        {
//    //            linesCurrentlyAttachedTo.Add(obj);
//    //        }
//    //    }
//    //}

//}
