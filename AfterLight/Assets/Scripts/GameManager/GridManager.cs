using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType { Generator, Storage, Consumer }

public class GridManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; set; }
    static float id;

    //List<PowerNode> workingNodeList = new List<PowerNode>();
    //List<GameObject> workingLineList = new List<GameObject>();
    Dictionary<string, PowerNode> workingNodeDic = new Dictionary<string, PowerNode>();
    Dictionary<string, Line> workingLineDic = new Dictionary<string, Line>();
    PowerGrid powerGridInstance;
    float workingTotalResistance;
    public bool isDrawingLine;
    GameObject powerGridPrefab;
    GameObject player;
    GameObject startObject; //holds the object starting the connection
    GameObject endObject;//holds the object ending the connection
    PowerNode startingNode;//holds the script of the starting object
    PowerNode endingNode;//holds the script of the ending object
    Vector3 lineRendStart;
    Vector3 lineRendEnd;
    GameObject linePrefab;
    PlayerManager playerManager;
    //  IPoweredObject start;
    //  IPoweredObject end;
    // GameObject currentlyConnectedObject;
    LineRenderer drawingLineRend;
    Material drawingLineMaterial;


    public void StartManager()
    {
        playerManager = GameManager.Player;
        //this needs to be fixed;
        playerManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        powerGridPrefab = Resources.Load("Prefabs/PowerGrid") as GameObject;
        linePrefab = Resources.Load("Prefabs/Line") as GameObject;
        drawingLineMaterial = Resources.Load("Materials/DrawingLine", typeof(Material)) as Material;

        drawingLineRend = GetComponent<LineRenderer>();
        drawingLineRend.GetComponent<Renderer>().material.color = Color.green;// drawingLineMaterial;
       



        status = ManagerStatus.Started;

    }
    private void Update()
    {
        if (isDrawingLine)
        {
            drawingLineRend.SetPosition(1, player.transform.position);
        }
        if (Input.GetButtonUp("Cancel") && isDrawingLine)
        {
            drawingLineRend.enabled = false;
            isDrawingLine = false;
            StartCoroutine(playerManager.ReleaseInteractionButtonLock());
        }
    }

    public void StartConnectingLine(GameObject start)
    {
        print("start connecting");
        startObject = start;
        playerManager.GetInteractionButtonLock();
        lineRendStart = startObject.transform.position;
        //lineRend = startObject.GetComponent<LineRenderer>();
        drawingLineRend.enabled = true;
        drawingLineRend.SetPosition(0, startObject.transform.position);
        isDrawingLine = true;
    }
    public void EndConnectingLine(GameObject endObject)
    {
      
        print("EndConnectingLine");
        //endObject = end;
        startingNode = startObject.GetComponentInParent<PowerNode>();
        //endingNode = endObject.GetComponentInParent<PowerNode>();
        endingNode = endObject.GetComponentInParent<PowerNode>();
        if (CheckIncomingLineForDuplicateConnection(startingNode, endingNode)) return;
        // startingNode.AddToConnectedNodesList(endingNode);
        // endingNode.AddToConnectedNodesList(startingNode);
        CreateNewLine(startObject, endObject);


        //if both objects arent attached to a grid
        if (startingNode.GetPowerGrid() == null && endingNode.GetPowerGrid() == null)
        {
            print("both null");
            //this will find all nodes connected and put them in a newly created grid
            StartCoroutine(CreateNewGrid(startingNode));
            //StartCoroutine(FindConnectedNodesAndLines(startingNode,null,CreateNewGrid()));
        }
        else if (startingNode.GetPowerGrid() != null && endingNode.GetPowerGrid() == null)
        {
            print("start is not null, end is null");
            startingNode.GetPowerGrid().AddNodeToGrid(endingNode);

        }
        else if(startingNode.GetPowerGrid() == null && endingNode.GetPowerGrid() != null)
        {
            print("start is null, end is not null");
            endingNode.GetPowerGrid().AddNodeToGrid(startingNode);
        }
        else if(startingNode.GetPowerGrid() != null && endingNode.GetPowerGrid() != null)
        {
            print("neither is null");

            // merge grids, esentailly create a new grid composed of the merged grids
            Destroy(startingNode.GetPowerGrid().gameObject);
            Destroy(endingNode.GetPowerGrid().gameObject);
            StartCoroutine(CreateNewGrid(startingNode));
        }

    }

    void CreateNewLine(GameObject origin, GameObject end)
    {
        print("CreateNewLine(0");
        drawingLineRend.enabled = false; // turn off temp line
        GameObject lineInstance = Instantiate(linePrefab, transform);// create permanant line object
        lineInstance.name = "Line" + AssignUniqueId();
        print("made new line: " + lineInstance.name);
        LineRenderer newLineRend = lineInstance.GetComponent<LineRenderer>();// get the lineRenderer
        lineInstance.GetComponent<Line>().SetLength(origin.transform.position, end.transform.position);// set the lenght of the line
        Line lineScript = lineInstance.GetComponent<Line>();
        lineScript.SetLength(origin.transform.position, end.transform.position);

        newLineRend.SetPosition(0, origin.transform.position);
        newLineRend.SetPosition(1, end.transform.position);
       
        AddColliderToLine(newLineRend, origin.transform.position, end.transform.position);

        origin.GetComponent<PowerNode>().AddToLineList(lineScript);// the nodes now know what line connecteed them
        end.GetComponent<PowerNode>().AddToLineList(lineScript);

        lineScript.AddNodeToList(origin.GetComponent<PowerNode>());// the lines know what nodes they connect
        lineScript.AddNodeToList(end.GetComponent<PowerNode>());
        isDrawingLine = false;// done drawing

        StartCoroutine(playerManager.ReleaseInteractionButtonLock());// release lock
    }
    IEnumerator CreateNewGrid(PowerNode startNode)
    {
        yield return new WaitForEndOfFrame();
        workingLineDic.Clear();
        workingNodeDic.Clear();
        startingNode.GetLineList().RemoveAll(i => i == null);
        // to stop creating powerGrids for solitary nodes
        if (startingNode.GetLineList().Count != 0)
        {
           powerGridInstance = Instantiate(powerGridPrefab).GetComponent<PowerGrid>();
            powerGridInstance.name = "PowerGrid" + AssignUniqueId();
            print("Creating new grid: " + powerGridInstance.name + " from : " + startNode.name);
            // make sure all recusive call branches finish before initializing new PowerGrid
            yield return StartCoroutine(FindAllNodesConnected(powerGridInstance,
                 startNode, null, null));
            //set grid instance variables
            powerGridInstance.PopulateGrid(workingNodeDic, workingLineDic, workingTotalResistance);
        }
    }


    //traverse down all lines and find nodes, this method took time to make
    IEnumerator FindAllNodesConnected(PowerGrid powerGridInstance, PowerNode currentNode, PowerNode callingNode, Line callingLine)
    {
        print("called findallnodes from node: " + currentNode.name);
        workingNodeDic.Add(currentNode.name, currentNode);// add node to dic
        print("Adding currentNode: " + currentNode + " to grid: " + powerGridInstance);
        currentNode.SetPowerGrid(powerGridInstance);
        currentNode.GetLineList().RemoveAll(item => item == null);

        foreach (Line line in currentNode.GetLineList())// get all lines connected to this node
        {
          
            print(line.name + " : " + currentNode.name);
            //dont include the line we just came from or any other line we already went down
            if (!line.Equals(callingLine) || !workingLineDic.ContainsKey(line.name))
            {
                print(line + " " + callingLine + " should not be equal here");
                print("adding line: " + line + " from : " + currentNode.name + " to dic");
                workingLineDic.Add(line.name, line);// add line to dic
                workingTotalResistance += line.GetResistance();// total resistance
                foreach (PowerNode node in line.GetNodeList())// find all nodes connected to this line              
                {
                    print("checking nodeList from : " + line + " for other attached object: " + node.name);
                    print("if " + node.name + " != " + currentNode.name + " proceed");
                    if (!node.Equals(currentNode) || !workingNodeDic.ContainsKey(currentNode.name))
                    {
                        print(node + " " + currentNode + " should not be equal at this point");
                        yield return StartCoroutine(FindAllNodesConnected(powerGridInstance,
                        node, currentNode, line));
                    }
                }

            }
        }
    }
    //public IEnumerator CutLine(Line cutLine)
    //{
    //    PowerNode start, end;
    //    start = cutLine.GetNodeList()[0];
    //    end = cutLine.GetNodeList()[1];
    //    print("CutLineCalled");
    //    yield return new WaitForEndOfFrame();
    //    print("after frame");
    //}

    public void CutLine(Line cutLine)
    {
        PowerNode start, end;
        start = cutLine.GetNodeList()[0];
        end = cutLine.GetNodeList()[1];
        if (start.GetPowerGrid().gameObject != null) Destroy(start.GetPowerGrid().gameObject);
        if (end.GetPowerGrid().gameObject != null) Destroy(end.GetPowerGrid().gameObject);
        Destroy(cutLine.gameObject);

        print("calling create grid from done: " + start.name);
          StartCoroutine(CreateNewGrid(start));
        if (!workingNodeDic.ContainsKey(end.name))
        {
            print("calling create grid from node " + end.name);
            StartCoroutine(CreateNewGrid(end));
        }
       
     
       
        

        //what a waste of time not knowing that the damn line wasnt destroyed until end of frame



        //start.GetLineList().RemoveAll(i => i == null);
        //end.GetLineList().RemoveAll(i => i == null);
        //for (var i = start.GetLineList().Count - 1; i > -1; i--)
        //{
        //    if (start.GetLineList()[i] == null)
        //        start.GetLineList().RemoveAt(i);
        //}
        //for (var i = end.GetLineList().Count - 1; i > -1; i--)
        //{
        //    if (end.GetLineList()[i] == null)
        //        end.GetLineList().RemoveAt(i);
        //}

        // were going to create 2 new grid
        //print(start.GetPowerGrid().name);
        //print(end.GetPowerGrid().name);
        //
        //print("1");
        //yield return new WaitForEndOfFrame();
        //print("2");
        //print(start.GetPowerGrid().name);
        //print(end.GetPowerGrid().name);
        //start.SetPowerGrid(null);
        //end.SetPowerGrid(null);
        
        //on first pass this line will always be false, on second pass if second node is connected
        //to this grid through some other line then we don't create another new grid
        //if (powerGridInstance.GetConnectedNodeDic() != null)
        //{
        //    if (powerGridInstance.GetConnectedNodeDic().ContainsKey(node.name)) break;
        //}
        
    }

    //Im embarrassed to admit this method took a while to get right.
    private void AddColliderToLine(LineRenderer line, Vector3 startPoint, Vector3 endPoint)
    {
        BoxCollider lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider>();
        lineCollider.tag = "Line";
        lineCollider.isTrigger = true;
        lineCollider.transform.parent = line.transform; 
        float lineLength = Vector3.Distance(startPoint, endPoint);
        float lineWidth = line.endWidth; // get width of collider from line    
        // size of collider is set where X is length of line, Y is width of line
        //z is not important,the collider will be a trigger, I plan on letting the player walk through the lines
        lineCollider.size = new Vector3(lineLength, lineWidth, 1f);   

        Vector3 midPoint = (startPoint + endPoint) / 2;
        lineCollider.transform.position = midPoint;
        // its misleading using x and z as your points, thinking in trig terms its on a 2d plane of x and y
        // angle = the tangent of the slope, Atan2 gets the slope for me so we just plug the points in
        //float angle = Mathf.Rad2Deg * Mathf.Atan2((startPoint.z - endPoint.z), (startPoint.x - endPoint.x));
        float angle = (Mathf.Rad2Deg * Mathf.Atan2((endPoint.z - startPoint.z), (endPoint.x - startPoint.x)));
        angle *= -1; // I'm not sure why this has to be negative it just works
        lineCollider.transform.Rotate(0, angle, 0);
    }

    static string AssignUniqueId()
    {
        id++;
        return id.ToString();
    }

    //make sure not to make the same connection twice
     bool CheckIncomingLineForDuplicateConnection(PowerNode start, PowerNode end)
    {
        // I wish unity would clear nulls when game objects are destroyed ..... : (
        start.GetLineList().RemoveAll(item => item == null);
        end.GetLineList().RemoveAll(item => item == null);
        bool flag = false;
        foreach(Line line in start.GetLineList())
        {
            foreach(PowerNode node in line.GetNodeList())
            {
                if (node.Equals(end)) flag = true;
            }
        }
        isDrawingLine = false;
        drawingLineRend.enabled = false;
        StartCoroutine(playerManager.ReleaseInteractionButtonLock());
        return flag;
    }






}
