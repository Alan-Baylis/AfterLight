using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGrid : MonoBehaviour
{
    public int totalNodesConnected;

    [SerializeField] Dictionary<string, PowerNode> connectedNodeDic = new Dictionary<string, PowerNode>();
    Dictionary<string, Line> connectedLineDic = new Dictionary<string, Line>();
    [SerializeField] float totalResistance;
    [SerializeField] float totalPowerGenerated;
    [SerializeField] float totalPowerDraw;
    [SerializeField] float totalPowerStorage;
    [SerializeField] float gainPerStorageUnit;
    List<PowerNode> generatorNodeList = new List<PowerNode>();
    List<PowerNode> storageNodeList = new List<PowerNode>();
    List<PowerNode> consumerNodeList = new List<PowerNode>();
    List<PowerNode> liveStorageList = new List<PowerNode>();
    Color gridCanvasColor;

    bool isGridPowered;
    // Use this for initialization
    void Start()
    {
       
        gridCanvasColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        totalNodesConnected = connectedNodeDic.Count;
        //InvokeRepeating("UpdateGrid", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrid();
    }

    float CalculatePowerGainPerUnit()
    {
        totalPowerDraw = 0;
        totalPowerStorage = 0;
        totalPowerGenerated = 0;
        liveStorageList.Clear();
        gainPerStorageUnit = 0;

        foreach (PowerNode node in generatorNodeList)
        {
            // if(node != null)
            totalPowerGenerated += node.GetComponentInParent<Generator>().GetPowerGenerated();
        }
        foreach (PowerNode node in consumerNodeList)
        {
            // if (node.GetIsOn()) // not needed as Conusmer class will set powerdraw to zero is it is off;
            //   if (node != null)
            //  {
            Consumer consumer = node.GetComponentInParent<Consumer>();
            totalPowerDraw += consumer.GetPowerDraw();
            consumer.SetIsPowered(true);
            //   }
        }
        foreach (PowerNode node in storageNodeList)
        {
            //  if (node != null)
            {
                if (node.GetComponent<IPower>().GetIsOn())
                {
                    liveStorageList.Add(node);
                    totalPowerStorage += node.GetComponentInParent<Storage>().GetEnergyStored();
                }
            }
        }
        float totalPowerDrawAndResistance = totalPowerDraw + totalResistance;
        if (totalPowerDraw == 0) totalPowerDrawAndResistance = 0; // if current isnt flowing there is no resistance
        gainPerStorageUnit = (((totalPowerGenerated - totalPowerDrawAndResistance)
        / liveStorageList.Count) * Time.deltaTime);

        return gainPerStorageUnit;
    }

    void AddGainToStorageUnits(float gain)
    {
        // dont want to drain from empty batteries so I check if gain is positive or negative
        List<PowerNode> nodeList = gain > 0 ? storageNodeList : liveStorageList;
        foreach (PowerNode node in nodeList)
        {
            node.GetComponentInParent<Storage>().AddGainToStorage(gain);
        }
    }

    void UpdateGrid()
    {
        AddGainToStorageUnits(CalculatePowerGainPerUnit());
        totalNodesConnected = connectedNodeDic.Count;
        if (totalPowerStorage <= 0) PowerDownConsumers();
        if (totalPowerStorage > 0 && !isGridPowered) PowerUpConsumers();
    }
    void PowerDownConsumers()
    {
        foreach (PowerNode node in consumerNodeList)
        {
            node.GetComponent<Consumer>().SetIsPowered(false);
            isGridPowered = false;
        }
    }
    void PowerUpConsumers()
    {
        foreach (PowerNode node in consumerNodeList)
        {
            node.GetComponent<Consumer>().SetIsPowered(true);
            isGridPowered = true;
        }
    }


    void PopulateTypedNodeLists()
    {
        generatorNodeList.Clear();
        consumerNodeList.Clear();
        storageNodeList.Clear();
        foreach (PowerNode node in connectedNodeDic.Values)
        {
            node.SetGridCanvasColor(gridCanvasColor);
            node.SetGridNodeCount(connectedNodeDic.Count);
            switch (node.GetComponentInParent<IPower>().GetNodeType())
            {
                case NodeType.Generator:
                    print(node.GetComponentInParent<IPower>().GetNodeType() == NodeType.Generator);
                    generatorNodeList.Add(node);
                    break;
                case NodeType.Consumer:
                    consumerNodeList.Add(node);
                    break;
                case NodeType.Storage:
                    storageNodeList.Add(node);
                    break;
            }
        }

    }

    public float GetGainPerStorageUnit()
    {
        return gainPerStorageUnit;
    }

    //public void SetNodeList(List<PowerNode> nodeList)
    //{
    //    print("callTo powergrid: " + this.name + " SetNodeList with: " + nodeList.Count);       
    //    this.connectedNodeList = new List<PowerNode>(nodeList);
    //}
    //public void SetLineList(List<GameObject> lineList)
    //{
    //    this.connectedLineList = lineList;
    //}
    //public void SetTotalResistance(float totalResistance)
    //{
    //    this.totalResistance = totalResistance;
    //}

    public void PopulateGrid(Dictionary<string, PowerNode> workingNodeDic,
        Dictionary<string, Line> workingLineDic, float workingTotalResistance)
    {
        connectedNodeDic = new Dictionary<string, PowerNode>(workingNodeDic);
        connectedLineDic = new Dictionary<string, Line>(workingLineDic);
        totalResistance = workingTotalResistance;
        PopulateTypedNodeLists();
    }
    public void AddNodeToGrid(PowerNode node)
    {
        //add the new node to the grid
        if (!connectedNodeDic.ContainsKey(node.name))
        {
            connectedNodeDic.Add(node.name, node);
            node.SetPowerGrid(this);
        }
        foreach(Line line in node.GetLineList())
        {
            //Add the new line to the grid
            if (!connectedLineDic.ContainsKey(line.name)) connectedLineDic.Add(line.name, line);
        }
        // make sure to populate lists
        PopulateTypedNodeLists();
    }

    public Dictionary<string, PowerNode> GetConnectedNodeDic()
    {
        return connectedNodeDic;
    }
}
