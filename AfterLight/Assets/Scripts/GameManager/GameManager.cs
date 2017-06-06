using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(GUIManager))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EnemyManager))]


//[RequireComponent(typeof(InventoryManager))]
//[RequireComponent(typeof(ViewManager))]
//[RequireComponent(typeof(EnvironmentManager))]
//[RequireComponent(typeof(TransactionManager))]

public class GameManager : MonoBehaviour
{
    //public static ItemDatabase ItemDB { get; private set; }
    //public static InventoryManager Inventory { get; private set; }
    //public static ViewManager ViewManager { get; private set; }
    //public static EnvironmentManager Environment { get; private set; }
    public static PlayerManager Player { get; private set; }
    public static GridManager Grid { get; private set; }
    public static GUIManager GUI { get; private set; }
    public static EnemyManager Enemy { get; private set; }
   
    //public static NPCManager NPC { get; private set; }
    //public static MouseClickController Mouse { get; private set; }
    //public static TransactionManager Transaction { get; private set; }

    private List<IGameManager> startSequenceList;

    private void Awake()
    {
        //ItemDB = GetComponent<ItemDatabase>();
        //Inventory = GetComponent<InventoryManager>();
        //ViewManager = GetComponent<ViewManager>();
        //Environment = GetComponent<EnvironmentManager>();
        Player = GetComponent<PlayerManager>();
        Grid = GetComponent<GridManager>();
        GUI = GetComponent<GUIManager>();
        Enemy = GetComponent<EnemyManager>();
    
        //NPC = GetComponent<NPCManager>();
        //Mouse = GetComponent<MouseClickController>();
        //Transaction = GetComponent<TransactionManager>();


        startSequenceList = new List<IGameManager>();

        //startSequenceList.Add(Inventory);
        //startSequenceList.Add(ViewManager);
        //startSequenceList.Add(Environment);
        startSequenceList.Add(Player);
        startSequenceList.Add(Grid);
        startSequenceList.Add(GUI);
        startSequenceList.Add(Enemy);
        //startSequenceList.Add(NPC);
        //startSequenceList.Add(Mouse);
        //startSequenceList.Add(Transaction);


      //  StartCoroutine(StartDatabaseManager());
        StartCoroutine(StartupManagers());

    }

   
    //function will block until Database is loaded from JSON
    //IEnumerator StartDatabaseManager()
    //{
    //    ItemDB.StartManager();
    //    while (ItemDB.status != ManagerStatus.Started)
    //    {
    //        yield return null;
    //    }
    //    Debug.Log("Database Loaded");
    //}

    private IEnumerator StartupManagers()
    {
        foreach (var manager in startSequenceList)
        {
            manager.StartManager();
        }
        yield return null;
        int numModules = startSequenceList.Count;
        //Debug.Log("Num of Modules: " + numModules);
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in startSequenceList)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log("Managers Loaded: " + numReady + "/" + numModules);
            yield return null;
        }
        Debug.Log("All managers started");
    }
}


