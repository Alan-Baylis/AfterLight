using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; set; }
    GameObject player;
    PlayerMovement playerMovement;
    PlayerShoot playerShoot;
    [SerializeField] bool isInteractionBusy;
    [SerializeField] bool isHoldingWireTool = false;
    [SerializeField] bool isHoldingWireCuttingTool = false;
    //public bool isConnectingBattery;
    // public Battery connectedBattery;
    public void StartManager()
    {
        //init
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerShoot = player.GetComponent<PlayerShoot>();
 
        status = ManagerStatus.Started;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetInteractionButtonLock()
    {
        isInteractionBusy = true;
    }
    public bool GetIsInteractionButtonLocked()
    {
        return isInteractionBusy;
    }
    public bool GetIsHoldingWireTool()
    {
        return isHoldingWireTool;
    }
    public bool GetIsHoldingWireCuttingTool()
    {
        return isHoldingWireCuttingTool;
    }

    public void TakeMouse()
    {
        playerMovement.isHasMouseControl = false;
        playerShoot.isHasMouseControl = false;
    }
    public void ReturnMouse()
    {
        playerMovement.isHasMouseControl = true;
        playerShoot.isHasMouseControl = true;
    }
    public void ToggleWireTool()
    {
        if (isHoldingWireTool)
        {
            isHoldingWireTool = false;
        }
        else
        {
            isHoldingWireTool = true;
        }
    }
    public void ToggleWireCuttingTool()
    {
        if (isHoldingWireCuttingTool)
        {
            isHoldingWireCuttingTool = false;
        }
        else
        {
            isHoldingWireCuttingTool = true;
        }
    }

    public IEnumerator ReleaseInteractionButtonLock()
    {
        yield return new WaitForEndOfFrame();
        isInteractionBusy = false;
    }
}

