using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PowerNode))]
public class Consumer : MonoBehaviour, IPower
{
    GridManager gridManager;
    PlayerManager playerManager;
    NodeType nodeType = NodeType.Consumer;
    PowerNode thisNode;

    [SerializeField] float powerDraw = 0;
    bool isConnected;
    //[SerializeField] PowerGrid powerGrid;
    //List<GameObject> linesCurrentlyAttachedTo = new List<GameObject>();
    [SerializeField] bool isPowered;
    //[SerializeField] bool isLightOn;
    [SerializeField] bool isOn;
    [SerializeField] bool isPlayerNear;


    public float GetPowerDraw()
    {
        return powerDraw;
    }


    //public bool IsDrawingPower
    //{
    //    get { return isLightOn; }
    //    set { hasPower = value; }
    //}
    //public bool IsBattery
    //{
    //    get { return IsBattery = false; }
    //    set { }
    //}
    //public string ObjectName
    //{
    //    get { return gameObjectName; }
    //    set { }
    //}
    //public bool IsPowered
    //{
    //    get { return hasPower; }
    //    set { hasPower = value; }
    //}
    //public bool IsPlayerNear
    //{
    //    get { return isPlayerNear; }
    //    set { isPlayerNear = value; }
    //}

    private void Start()
    {

        gridManager = GameManager.Grid;
        playerManager = GameManager.Player;
        thisNode = GetComponentInParent<PowerNode>();
        //thisNode.SetNodeType(nodeType);
        //playerManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();




    }
    private void Update()
    {
        CheckForIncomingLines();
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

    public bool GetIsPlayerNear()
    {
        return isPlayerNear;
    }
    public void SetPowerDraw(float powerDraw)
    {
        this.powerDraw = powerDraw;
    }

    public NodeType GetNodeType()
    {
        return nodeType;
    }
    public void SetIsPlayerNear(bool boolean)
    {
        isPlayerNear = boolean;
    }

    //public void DrawPower(GameObject batteryToDrawFrom)
    //{
    //     power.DrawPower(batteryToDrawFrom);
    //}

    //void CheckIfPowered()
    //{
    //    if (!isPowered && isLightOn)
    //    {
    //        TurnOffLight();
    //    }
    //    if (isPowered && !isLightOn)
    //    {
    //        TurnOnLight();
    //    }
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
    //    }
    //}

    //    public PowerGrid GetPowerGridAttachedTo()
    //    {
    //        return powerGrid;
    //    }

    //    public void AddToLinesCurrentlyAttached(GameObject line)
    //    {
    //        linesCurrentlyAttachedTo.Add(line);
    //    }

    //    public List<GameObject> GetLinesCurrentlyAttached()
    //    {
    //        return linesCurrentlyAttachedTo;
    //    }

    //    public void SetPowerGridAttachedTo(PowerGrid powerGrid)
    //    {
    //        this.powerGrid = powerGrid;
    //    }

    //    public void ClearNullsOutOfLineList()
    //    {
    //        List<GameObject> temp = new List<GameObject>();
    //        foreach (var obj in linesCurrentlyAttachedTo)
    //        {
    //            if (obj != null)
    //            {
    //                temp.Add(obj);
    //            }
    //        }
    //        linesCurrentlyAttachedTo.Clear();
    //        foreach (var obj in temp)
    //        {
    //            if (obj != null)
    //            {
    //                linesCurrentlyAttachedTo.Add(obj);
    //            }
    //        }
    //    }

    //}
    public bool GetIsOn()
    {
        return isOn;
    }
    public void SetIsPowered(bool boolean)
    {
        isPowered = boolean;
    }
    public bool GetIsPowered()
    {
        return isPowered;
    }
}
