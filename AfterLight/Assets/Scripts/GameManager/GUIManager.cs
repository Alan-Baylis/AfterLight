using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; set; }
    PlayerManager playerManager;
   
    GameObject mainCanvas;
    //batteryPanel
    GameObject batteryPanel;
    Text batteryEnergyText;
    Text currentDrawText;
    Slider batterySlider;
    Image batteryFillImage;
    Storage currentStorage;
    Button cutAllLinesButton;
    Vector2 middleOfScreen = new Vector2((Screen.width / 2), (Screen.height / 2));
    //
   
   

    public bool isBatteryGUIOpen;
    // Use this for initialization
    public void StartManager()
    {
        playerManager = GameManager.Player;
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        //Battery GUI
        batteryPanel = mainCanvas.transform.Find("BatteryPanel").gameObject;
        batteryEnergyText = batteryPanel.transform.Find("EnergyText").gameObject.GetComponent<Text>();
        currentDrawText = batteryPanel.transform.Find("CurrentDrawText").gameObject.GetComponent<Text>();
        batterySlider = batteryPanel.transform.Find("BatterySlider").gameObject.GetComponent<Slider>();
        cutAllLinesButton = batteryPanel.transform.Find("CutLinesButton").GetComponent<Button>();
        batteryFillImage = batterySlider.transform.Find("Fill Area").
            transform.Find("Fill").GetComponent<Image>();


        batteryPanel.SetActive(false);

        status = ManagerStatus.Started;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            
           // cutAllLinesButton.onClick.AddListener(() => currentStorage.CutAllLines());
           
        }   
        if (isBatteryGUIOpen)
        {
            UpdateBatteryUI();
        }
    }
    public void CloseBatteryGUI()
    {
        isBatteryGUIOpen = false;
        batteryPanel.SetActive(false);
        playerManager.ReturnMouse();
    }
    public void OpenBatteryGUI(Storage battery)
    {
        isBatteryGUIOpen = true;
        currentStorage = battery;
        playerManager.TakeMouse();
        //batteryPanel.transform.position = Input.mousePosition;
        batteryPanel.transform.position = middleOfScreen;
        batteryPanel.SetActive(true);
    }
    void UpdateBatteryUI()
    {
        batterySlider.value = currentStorage.GetEnergyStored();
        batteryFillImage.color = Color.Lerp(currentStorage.emptyBatteryColor,
            currentStorage.fullBatteryColor, currentStorage.GetEnergyStored() / currentStorage.GetMaxEnergy());
        batteryEnergyText.text = ((int)currentStorage.GetEnergyStored()).ToString() + "%";
        //check to see if battery is attached to a power grid
        
        if (currentStorage.GetComponent<PowerNode>().GetPowerGrid() != null)
        {
            currentDrawText.text = "Current Draw: " + currentStorage.GetComponent<PowerNode>().GetPowerGrid().
                GetGainPerStorageUnit().ToString();
        }
        else
        {
            currentDrawText.text = "Current Draw: 0";
        }
        
        
    }
}
