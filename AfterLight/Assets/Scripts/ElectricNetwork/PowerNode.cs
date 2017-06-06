using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerNode : MonoBehaviour {
    [SerializeField] private PowerGrid powerGrid;
    [SerializeField] private List<Line> connectedLineList = new List<Line>();
    GameObject gridCanvas;
    GameObject gridPanel;
    GameObject gridText;
    Color gridCanvasColor = Color.white;
    int gridNodeCount = 0;
    

    //private List<PowerNode> connectedNodes = new List<PowerNode>();
    private void Start()
    {
        gridCanvas = transform.Find("GridCanvas").gameObject as GameObject;
        gridPanel = gridCanvas.transform.Find("Panel").gameObject as GameObject;
        gridText = gridPanel.transform.Find("Text").gameObject as GameObject;
    }
    private void Update()
    {
        if (powerGrid != null)
            gridText.GetComponent<Text>().text = powerGrid.name; //+ "\n" + "Nodes: " + gridNodeCount;
        gridPanel.GetComponent<Image>().color = gridCanvasColor;
       
    }

    public PowerGrid GetPowerGrid()
    {
        return powerGrid;
    }
    public void SetPowerGrid(PowerGrid powerGrid)
    {
        this.powerGrid = powerGrid;
    }
    public List<Line> GetLineList()
    {
        return connectedLineList;
    }
    public void SetLineList(List<Line> lineList)
    {
        this.connectedLineList = lineList; 
    }
    public void AddToLineList(Line lineToAdd)
    {
        connectedLineList.Add(lineToAdd);
    }
    public void SetGridCanvasColor(Color color)
    {
        gridCanvasColor = color;
    }
    public void SetGridNodeCount(int num)
    {
        gridNodeCount = num;
    }
    //public List<PowerNode> GetConnectedNodesList()
    //{
    //    return connectedNodes;
    //}
    //public void AddToConnectedNodesList(PowerNode node)
    //{
    //    connectedNodes.Add(node);
    //}
    //public void ClearAllLists()
    //{
    //    connectedNodes.Clear();
    //    connectedLineList.Clear();
    //}
    //public bool GetIsOn()
    //{
    //    return isOn;
    //}
    //public void SetIsOn(bool boolean)
    //{
    //    isOn = boolean;
    //}
    //public void SetNodeType(NodeType nodeType)
    //{
    //    this.nodeType = nodeType;
    //}
    //public NodeType GetNodeType()
    //{
    //    return nodeType;
    //}

}
