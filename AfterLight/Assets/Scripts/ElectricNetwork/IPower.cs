using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPower  {
    NodeType GetNodeType();

    bool GetIsOn();

    void CheckForIncomingLines();
    void CheckForOutGoingLines();
    void SetIsPlayerNear(bool boolean);
    
 
}
