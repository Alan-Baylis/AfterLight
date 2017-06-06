using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
    PlayerManager playerManager;
    GridManager gridManager;
    List<PowerNode> nodeList = new List<PowerNode>();
    [SerializeField]float length;
    //float area = 1.02f;
    float resistivity = .005f;
    float resistance;
    float startWidth;
    float endWidth;
    [SerializeField] bool isPlayerNear;
    BoxCollider thisBoxCollider;
    LineRenderer thisLineRend;

    Renderer rend;
    Material defaultMaterial;
    Material cuttingMaterial;
    private void Start()
    {
        playerManager = GameManager.Player;
        gridManager = GameManager.Grid;
        thisLineRend = GetComponent<LineRenderer>();
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
        startWidth = thisLineRend.startWidth;
        endWidth = thisLineRend.endWidth;
    }
    private void Update()
    {
        if(isPlayerNear && playerManager.GetIsHoldingWireCuttingTool())
        {
            HighlightWire();
        }
        if (!isPlayerNear)
        {
            UnHighlightWire();
        }
        if(Input.GetButtonUp("Submit") && !playerManager.GetIsInteractionButtonLocked() &&
            playerManager.GetIsHoldingWireCuttingTool() && isPlayerNear)
        {
            gridManager.CutLine(this);
        }
    }
    void HighlightWire()
    {
        //  thisLineRend.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        //thisLineRend.material.color = Color.white;
        rend.material = cuttingMaterial;
    }
    void UnHighlightWire()
    {
        rend.material = defaultMaterial;
    }
    public float GetResistance()
    {
        //  return (resistivity * length) / area;  
        return (resistivity * length);
    }
    public void SetLength(Vector3 start, Vector3 end)
    {
        length = Vector3.Distance(start, end);
    }
    public void AddNodeToList(PowerNode node)
    {
        nodeList.Add(node);
    }
    public List<PowerNode> GetNodeList()
    {
        return nodeList;
    }
    public void SetIsPlayerNear(bool boolean)
    {
        isPlayerNear = boolean;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    SetIsPlayerNear(true);
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    SetIsPlayerNear(false);
    //}


}
