//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace other
//{
//    public class PowerGrid : MonoBehaviour
//    {
//        [SerializeField] int numOfLiveBatteries;
//        [SerializeField] float totalPowerDraw;
//        [SerializeField] float powerDrawForEachBattery = 0;
//        [SerializeField] float totalPower;
//        bool isPoweredDown;
//        Dictionary<string, IPoweredObject> batteriesConnected = new Dictionary<string, IPoweredObject>();
//        List<Battery> liveBatteriesConnected = new List<Battery>();
//        Dictionary<string, IPoweredObject> nonBatteriesConnected = new Dictionary<string, IPoweredObject>();

//        // Use this for initialization
//        void Start()
//        {
//            //  PopulateLiveBatteryList();
//        }

//        // Update is called once per frame
//        void Update()
//        {
//            GetToatlPowerAndDrawFromBatteries();
//        }
//        public void PopulateLiveBatteryList()
//        {
//            PrintBatteriesConnected();
//            liveBatteriesConnected.Clear();
//            numOfLiveBatteries = 0;
//            foreach (Battery batt in batteriesConnected.Values)
//            {
//                if (!batt.isDead)
//                {
//                    liveBatteriesConnected.Add(batt);
//                }
//            }
//            numOfLiveBatteries = liveBatteriesConnected.Count;
//        }
//        void GetToatlPowerAndDrawFromBatteries()
//        {
//            totalPower = 0;
//            foreach (Battery batt in liveBatteriesConnected)
//            {
//                totalPower += batt.energy;
//                batt.DrainFromBattery(powerDrawForEachBattery);
//            }
//            if (totalPower <= 0 && !isPoweredDown) PowerDown();
//            if (totalPower > 0 && isPoweredDown) PowerBackOn();
//        }
//        public void CalculatePowerDraw()
//        {
//            totalPowerDraw = 0;
//            foreach (var poweredObject in nonBatteriesConnected.Values)
//            {
//                if (poweredObject.IsDrawingPower)
//                {
//                    totalPowerDraw += poweredObject.GetPowerDraw();
//                }
//            }
//            powerDrawForEachBattery = totalPowerDraw / numOfLiveBatteries;
//        }

//        public void AddNonBatteryToGrid(string key, IPoweredObject poweredObject)
//        {
//            print("adding non battery to grid with key: " + key + " and value: " + poweredObject.ObjectName);
//            nonBatteriesConnected.Add(key, poweredObject);
//            poweredObject.IsDrawingPower = true;
//            CalculatePowerDraw();
//        }

//        public void AddBatteryToGrid(string key, IPoweredObject batteryToAdd)
//        {
//            print("adding battery to grid with key: " + key + " and value: " + batteryToAdd.ObjectName);
//            batteriesConnected.Add(key, batteryToAdd);
//            PopulateLiveBatteryList();
//        }


//        public Dictionary<string, IPoweredObject> GetBatteriesConnected()
//        {
//            return batteriesConnected;
//        }
//        public Dictionary<string, IPoweredObject> GetPoweredObjectsConnected()
//        {
//            return nonBatteriesConnected;
//        }
//        public float GetPowerDrawForEachBattery()
//        {
//            return powerDrawForEachBattery;
//        }
//        public void RemoveFromGrid(IPoweredObject powerObject)
//        {
//            print("call to removefromgrid");
//            if (powerObject.IsBattery)
//            {
//                print("removing battery from grid: " + powerObject.ObjectName);
//                bool d = batteriesConnected.Remove(powerObject.ObjectName);
//                print(d);
//            }
//            else
//            {
//                print("removing object from grid: " + powerObject.ObjectName);
//                bool k = nonBatteriesConnected.Remove(powerObject.ObjectName);
//                print(k);
//            }
//            PopulateLiveBatteryList();
//            CalculatePowerDraw();
//        }
//        public void PrintBatteriesConnected()
//        {
//            foreach (KeyValuePair<string, IPoweredObject> batt in batteriesConnected)
//            {
//                print("Key: " + batt.Key + " Value: " + batt.Value);
//            }
//        }
//        public void PowerDown()
//        {
//            //iterate thorugh connected nonBattery objects and turn them off
//            foreach (var poweredObject in nonBatteriesConnected.Values)
//            {
//                poweredObject.IsPowered = false;
//                isPoweredDown = true;
//            }
//        }
//        public void PowerBackOn()
//        {
//            foreach (var poweredObject in nonBatteriesConnected.Values)
//            {
//                poweredObject.IsPowered = true;
//                isPoweredDown = false;
//            }
//        }
//        //when deleting a node between two nodes I need to make sure the other two are off the grid
//        public void CheckToSeeIfGridWasBroken()
//        {
//            print("Checktoseeifgridwasbroken");
//            foreach (var obj in nonBatteriesConnected.Values)
//            {
//                obj.ClearNullsOutOfLineList();
//                if (obj.GetLinesCurrentlyAttached().Count == 0)
//                {
//                    obj.SetPowerGridAttachedTo(null);
//                }
//            }
//            foreach (var obj in batteriesConnected.Values)
//            {
//                obj.ClearNullsOutOfLineList();
//                if (obj.GetLinesCurrentlyAttached().Count == 0)
//                {
//                    obj.SetPowerGridAttachedTo(null);
//                }
//            }
//        }

//    }
//}
