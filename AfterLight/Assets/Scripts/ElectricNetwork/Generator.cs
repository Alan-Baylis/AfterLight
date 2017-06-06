using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour,IPower
{
    GUIManager GUI;
    GridManager gridManager;
    PlayerManager playerManager;
    NodeType nodeType = NodeType.Generator;
    PowerNode thisNode;
    float powerGeneration = 2;
    bool isOn;
    [SerializeField] bool isPlayerNear;



    void Start()
    {
        playerManager = GameManager.Player;
        gridManager = GameManager.Grid;
        GUI = GameManager.GUI;
        thisNode = GetComponent<PowerNode>();
    }

    void Update()
    {
        CheckForIncomingLines();
        CheckForOutGoingLines();

        //if (Input.GetButtonUp("Submit") && isPlayerNear && !playerManager.GetIsHoldingWireTool())
        //{
        //    GUI.OpenBatteryGUI(this);
        //}    
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

    public float GetPowerGenerated()
    {
        return powerGeneration;
    }

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
}
