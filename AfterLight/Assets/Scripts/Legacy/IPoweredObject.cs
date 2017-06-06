using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoweredObject{
    //float GetPowerDraw();
    bool IsBattery { set; get; }
    string ObjectName { set; get; }
    bool IsDrawingPower { set; get; }
    bool IsPowered { get; set; }
    bool IsPlayerNear { get; set; }
    //List<GameObject>LinesCurrentlyAttached;

    PowerGrid GetPowerGridAttachedTo();
    void ClearNullsOutOfLineList();
    float GetPowerDraw();
    void AddToLinesCurrentlyAttached(GameObject line);
    List<GameObject> GetLinesCurrentlyAttached();
   
    void SetPowerGridAttachedTo(PowerGrid powerGrid);
}
