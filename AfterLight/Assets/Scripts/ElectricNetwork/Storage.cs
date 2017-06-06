using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PowerNode))]
public class Storage : MonoBehaviour,IPower {
    //SmallLight lightPlane;
    GridManager gridManager;
    PlayerManager playerManager;
    GUIManager GUI;
    NodeType nodeType = NodeType.Storage;
    PowerNode thisNode;

    //[SerializeField] private PowerGrid powerGrid;
    //List<GameObject> linesCurrentlyAttached = new List<GameObject>();
    // public bool isDead = false;
    [SerializeField] private float energy = 100;
    [SerializeField] float maxEnergy = 100;
    [SerializeField] float energyDrainAmount;
    [SerializeField] bool isPlayerNear;
    bool isOn = true;


    //string gameObjectName;

    //firstCanvas
    Slider batterySlider;
    Image fillImage;
    public Color fullBatteryColor = Color.green;
    public Color emptyBatteryColor = Color.red;



    //public string ObjectName { get { return gameObjectName; } set { } }
    //public bool IsBattery { get { return this.IsBattery = true; } set { } }
    //public bool IsDrawingPower { get; set; }
    //public bool IsPowered { get; set; }
    //public bool IsPlayerNear { get { return isPlayerNear; } set { } }

    // Use this for initialization
    void Start()
    {
        playerManager = GameManager.Player;
        gridManager = GameManager.Grid;
        GUI = GameManager.GUI;

        thisNode = GetComponentInParent<PowerNode>();

        batterySlider = transform.Find("Canvas").transform.Find("BatterySlider").GetComponent<Slider>();
        fillImage = batterySlider.transform.Find("FillArea").transform.Find("Fill").GetComponent<Image>();

        //gameObjectName = gameObject.name;

    }

    void Update()
    {
        CheckForIncomingLines();
        CheckBatteryStatus();
        SetBatteryUI();
        CheckForOutGoingLines();   
    }

    public void CheckForIncomingLines()
    {
        if (Input.GetButtonUp("Submit") && isPlayerNear && gridManager.isDrawingLine)
        {
            gridManager.EndConnectingLine(gameObject);
        }
    }

    public void CheckForOutGoingLines()
    {
        if (Input.GetButtonUp("Submit") && isPlayerNear &&
            playerManager.GetIsHoldingWireTool() && !playerManager.GetIsInteractionButtonLocked()) //&& !player.GetInteractionBusy())
        {
            gridManager.StartConnectingLine(gameObject);
        }
    }

    void CheckBatteryStatus()
    {
        if (energy <= 0 && isOn) isOn = false;
        if (energy < 0) energy = 0;
        if (energy > maxEnergy) energy = maxEnergy;
        if (energy > 0 && !isOn) isOn = true;
    }

    

    public void CheckForGUIRequest()
    {
        if (Input.GetButtonUp("Submit") && isPlayerNear && !playerManager.GetIsHoldingWireTool())
        {
            GUI.OpenBatteryGUI(this);
        }
        if (Input.GetButtonUp("Cancel") && GUI.isBatteryGUIOpen)
        {
            GUI.CloseBatteryGUI();
        }
    }

    public void AddGainToStorage(float gain)
    {
        energy += gain;
    }

    //void BatteryDead()
    //{
    //    thisNode.SetIsOn(false);
    //}

    void SetBatteryUI()
    {
        batterySlider.value = energy;
        fillImage.color = Color.Lerp(emptyBatteryColor, fullBatteryColor, energy / maxEnergy);
    }

    public float GetEnergyStored()
    {
        return energy;
    }

    public float GetMaxEnergy()
    {
        return maxEnergy;
    }

   

    //public void SetPowerGridAttachedTo(PowerGrid powerGrid)
    //{
    //    this.powerGrid = powerGrid;
    //}
    //public PowerGrid GetPowerGridAttachedTo()
    //{
    //    return powerGrid;
    //}

    //public float GetPowerDraw()
    //{
    //    return 0;
    //}
    //public List<GameObject> GetLinesCurrentlyAttached()
    //{
    //    return linesCurrentlyAttached;
    //}
    //public void AddToLinesCurrentlyAttached(GameObject line)
    //{
    //    linesCurrentlyAttached.Add(line);
    //}
    //public void CutAllLines()
    //{
    //    foreach (GameObject line in linesCurrentlyAttached)
    //    {
    //        print("cutting line: " + line.name);
    //        Destroy(line);
    //    }
    //    linesCurrentlyAttached.Clear();
    //    if (powerGrid != null) powerGrid.RemoveFromGrid(this);
    //    print("powergrid: " + powerGrid);
    //    powerGrid.CheckToSeeIfGridWasBroken();
    //    powerGrid = null;
    //    GUI.CloseBatteryGUI();

    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        isPlayerNear = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        isPlayerNear = false;
    //        GUI.CloseBatteryGUI();
    //    }
    //}
    //public void ClearNullsOutOfLineList()
    //{
    //    List<GameObject> temp = new List<GameObject>();
    //    foreach (var obj in linesCurrentlyAttached)
    //    {
    //        if (obj != null)
    //        {
    //            temp.Add(obj);
    //        }
    //    }
    //    linesCurrentlyAttached.Clear();
    //    foreach (var obj in temp)
    //    {
    //        if (obj != null)
    //        {
    //            linesCurrentlyAttached.Add(obj);
    //        }
    //    }
    //}
    //public float GetEnergyStored()
    //{
    //    return energy;
    //}

    public NodeType GetNodeType()
    {
        return nodeType;
    }
    public bool GetIsOn()
    {
        return isOn;
    }
    public void SetIsPlayerNear(bool boolean)
    {
        isPlayerNear = boolean;
    }
}
