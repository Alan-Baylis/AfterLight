//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using  namespace legacy
//{
//    public class Battery : MonoBehaviour, IPoweredObject
//    {

//        SmallLight lightPlane;
//        GridManager gridManager;
//        PlayerManager playerManager;
//        GUIManager GUI;

//        [SerializeField] private PowerGrid powerGrid;
//        List<GameObject> linesCurrentlyAttached = new List<GameObject>();
//        public bool isDead = false;
//        public float energy = 100;
//        [HideInInspector] public float maxEnergy = 100;
//        bool isPlayerNear;
//        [SerializeField] float energyDrainAmount;
//        string gameObjectName;

//        //firstCanvas
//        Slider batterySlider;
//        Image fillImage;
//        public Color fullBatteryColor = Color.green;
//        public Color emptyBatteryColor = Color.red;



//        public string ObjectName { get { return gameObjectName; } set { } }
//        public bool IsBattery { get { return this.IsBattery = true; } set { } }
//        public bool IsDrawingPower { get; set; }
//        public bool IsPowered { get; set; }
//        public bool IsPlayerNear { get { return isPlayerNear; } set { } }

//        // Use this for initialization
//        void Start()
//        {
//            playerManager = GameManager.Player;
//            gridManager = GameManager.Grid;
//            GUI = GameManager.GUI;
//            batterySlider = transform.Find("Canvas").transform.Find("BatterySlider").GetComponent<Slider>();
//            fillImage = batterySlider.transform.Find("FillArea").transform.Find("Fill").GetComponent<Image>();

//            gameObjectName = gameObject.name;

//        }

//        void Update()
//        {
//            CheckForIncomingLines();
//            CheckBatteryStatus();
//            SetBatteryUI();

//            if (Input.GetButtonUp("Submit") && isPlayerNear &&
//                playerManager.GetIsHoldingWireTool() && !playerManager.GetIsInteractionButtonLocked()) //&& !player.GetInteractionBusy())
//            {
//                gridManager.StartConnectingLine(gameObject);
//            }

//            if (Input.GetButtonUp("Submit") && isPlayerNear && !playerManager.GetIsHoldingWireTool())
//            {
//                GUI.OpenBatteryGUI(this);
//            }
//            if (Input.GetButtonUp("Cancel") && GUI.isBatteryGUIOpen)
//            {
//                GUI.CloseBatteryGUI();
//            }
//        }
//        void CheckBatteryStatus()
//        {
//            if (energy <= 0 && !isDead) BatteryDead();
//            if (energy < 0) energy = 0;
//            if (energy > maxEnergy) energy = maxEnergy;
//        }
//        public void DrainFromBattery(float amount)
//        {
//            energy -= amount * Time.deltaTime;
//        }
//        void BatteryDead()
//        {
//            //when a battery dies it needs to tell the powergrid and repopulate the list
//            isDead = true;
//            powerGrid.PopulateLiveBatteryList();
//        }
//        void SetBatteryUI()
//        {
//            batterySlider.value = energy;
//            fillImage.color = Color.Lerp(emptyBatteryColor, fullBatteryColor, energy / maxEnergy);
//        }

//        void CheckForIncomingLines()
//        {
//            if (Input.GetButtonUp("Submit") && isPlayerNear && gridManager.isDrawingLine)
//            {
//                gridManager.EndConnectingLine(gameObject);
//            }
//        }

//        public void SetPowerGridAttachedTo(PowerGrid powerGrid)
//        {
//            this.powerGrid = powerGrid;
//        }
//        public PowerGrid GetPowerGridAttachedTo()
//        {
//            return powerGrid;
//        }

//        public float GetPowerDraw()
//        {
//            return 0;
//        }
//        public List<GameObject> GetLinesCurrentlyAttached()
//        {
//            return linesCurrentlyAttached;
//        }
//        public void AddToLinesCurrentlyAttached(GameObject line)
//        {
//            linesCurrentlyAttached.Add(line);
//        }
//        public void CutAllLines()
//        {
//            foreach (GameObject line in linesCurrentlyAttached)
//            {
//                print("cutting line: " + line.name);
//                Destroy(line);
//            }
//            linesCurrentlyAttached.Clear();
//            if (powerGrid != null) powerGrid.RemoveFromGrid(this);
//            print("powergrid: " + powerGrid);
//            powerGrid.CheckToSeeIfGridWasBroken();
//            powerGrid = null;
//            GUI.CloseBatteryGUI();

//        }

//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.tag == "Player")
//            {
//                isPlayerNear = true;
//            }
//        }
//        private void OnTriggerExit(Collider other)
//        {
//            if (other.tag == "Player")
//            {
//                isPlayerNear = false;
//                GUI.CloseBatteryGUI();
//            }
//        }
//        public void ClearNullsOutOfLineList()
//        {
//            List<GameObject> temp = new List<GameObject>();
//            foreach (var obj in linesCurrentlyAttached)
//            {
//                if (obj != null)
//                {
//                    temp.Add(obj);
//                }
//            }
//            linesCurrentlyAttached.Clear();
//            foreach (var obj in temp)
//            {
//                if (obj != null)
//                {
//                    linesCurrentlyAttached.Add(obj);
//                }
//            }
//        }


//    }
//}