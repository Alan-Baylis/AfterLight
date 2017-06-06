//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using namespace legacy
//{
//    public class GridManager : MonoBehaviour, IGameManager
//    {
//        public ManagerStatus status { get; set; }
//        public bool isDrawingLine;
//        GameObject powerGrid;
//        GameObject player;
//        GameObject lineStart;
//        GameObject line;
//        PlayerManager playerManager;
//        IPoweredObject start;
//        IPoweredObject end;


//        GameObject currentlyConnectedObject;
//        LineRenderer lineRend;


//        private void Update()
//        {
//            if (isDrawingLine)
//            {
//                lineRend.SetPosition(1, player.transform.position);
//            }
//            if (Input.GetButtonUp("Cancel") && isDrawingLine)
//            {
//                lineRend.enabled = false;
//                isDrawingLine = false;
//                StartCoroutine(playerManager.ReleaseInteractionButtonLock());
//            }
//        }


//        public void StartManager()
//        {
//            playerManager = GameManager.Player;
//            player = GameObject.FindGameObjectWithTag("Player");
//            powerGrid = Resources.Load("Prefabs/PowerGrid") as GameObject;
//            status = ManagerStatus.Started;
//            line = Resources.Load("Prefabs/Line") as GameObject;
//        }
//        public void StartConnectingLine(GameObject start)
//        {
//            playerManager.GetInteractionButtonLock();
//            print("startConnectingLine with start: " + start.name);
//            lineStart = start;
//            lineRend = start.GetComponent<LineRenderer>();
//            lineRend.enabled = true;
//            lineRend.SetPosition(0, start.transform.position);
//            isDrawingLine = true;
//        }


//        public void EndConnectingLine(GameObject lineEnd)
//        {
//            print("EndConnectingLine");
//            start = lineStart.GetComponent<IPoweredObject>();
//            end = lineEnd.GetComponent<IPoweredObject>();
//            PowerGrid startPowerGrid = start.GetPowerGridAttachedTo();
//            PowerGrid endPowerGrid = end.GetPowerGridAttachedTo();
//            //if both objects arent attached to a grid
//            if (startPowerGrid == null && endPowerGrid == null)
//            {
//                print("both null");
//                PowerGrid newGrid = CreateNewGrid();
//                start.SetPowerGridAttachedTo(newGrid);
//                end.SetPowerGridAttachedTo(newGrid);
//                if (start.IsBattery)
//                {
//                    newGrid.AddBatteryToGrid(start.ObjectName, start);
//                }
//                else
//                {
//                    newGrid.AddNonBatteryToGrid(start.ObjectName, start);
//                }

//                if (end.IsBattery)
//                {
//                    newGrid.AddBatteryToGrid(end.ObjectName, end);
//                }
//                else
//                {
//                    newGrid.AddNonBatteryToGrid(end.ObjectName, end);
//                }
//            }
//            //if start is !null and end is null
//            else if (startPowerGrid != null && endPowerGrid == null)
//            {
//                print("start is not null end is null");
//                end.SetPowerGridAttachedTo(startPowerGrid);
//                if (end.IsBattery)
//                {
//                    startPowerGrid.AddBatteryToGrid(end.ObjectName, end);
//                }
//                else
//                {
//                    startPowerGrid.AddNonBatteryToGrid(end.ObjectName, end);
//                }
//            }
//            //if start is null and end is !null;
//            else if (startPowerGrid == null && endPowerGrid != null)
//            {
//                start.SetPowerGridAttachedTo(endPowerGrid);

//                if (start.IsBattery)
//                {
//                    print("start is a battery");
//                    endPowerGrid.AddBatteryToGrid(start.ObjectName, start);
//                }
//                else
//                {
//                    print("start is not a battery");
//                    endPowerGrid.AddNonBatteryToGrid(start.ObjectName, start);
//                }
//            }
//            // if objects are on the same grid
//            else if (startPowerGrid.Equals(endPowerGrid))
//            {
//                print("same grid");

//            }
//            // if both objects have different grids
//            else
//            {
//                MergeGrids(startPowerGrid, endPowerGrid);
//                print("merge grids");
//            }
//            DoneDrawingLine(lineStart.transform, lineEnd.transform);
//        }
//        public PowerGrid CreateNewGrid()
//        {
//            GameObject powerGridInstance = Instantiate(powerGrid);
//            return powerGridInstance.GetComponent<PowerGrid>();
//        }

//        public void MergeGrids(PowerGrid start, PowerGrid end)
//        {
//            foreach (KeyValuePair<string, IPoweredObject> batt in start.GetBatteriesConnected())
//                end.AddBatteryToGrid(batt.Key, batt.Value);
//            foreach (KeyValuePair<string, IPoweredObject> poweredObject in start.GetPoweredObjectsConnected())
//                end.AddNonBatteryToGrid(poweredObject.Key, poweredObject.Value);
//            Destroy(start.gameObject);
//        }
//        void DoneDrawingLine(Transform begin, Transform finish)
//        {
//            lineRend.enabled = false;
//            GameObject lineInstance = Instantiate(line, transform);
//            LineRenderer newLineRend = lineInstance.GetComponent<LineRenderer>();
//            start.AddToLinesCurrentlyAttached(lineInstance);
//            end.AddToLinesCurrentlyAttached(lineInstance);
//            newLineRend.SetPosition(0, begin.position);
//            newLineRend.SetPosition(1, finish.position);
//            isDrawingLine = false;
//            StartCoroutine(playerManager.ReleaseInteractionButtonLock());
//        }

//    }
//}