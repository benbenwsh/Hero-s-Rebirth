using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    public int width;
    public int height;

    private Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    public List<Vector2> startNCombatRoomCoordinates = new List<Vector2>();
    public List<Vector2> allRoomCoordinates = new List<Vector2>();
    public List<GameObject> allRoomTypes = new List<GameObject>();

    public Dictionary<Vector2, List<Vector2>> doorLocations = new Dictionary<Vector2, List<Vector2>>();
    private Vector2 roomToExitRoom;

    public Vector2 currentRoomCoordinates = new Vector2(0, 0);

    public int gridGraphScanCount = 0;

    public GameObject room;
    public GameObject fixedEnemyCombatRoom;
    public GameObject randomEnemyCombatRoom;
    public GameObject chestRoom;
    public GameObject trappedChestRoom;
    public GameObject exitRoom;

    private CombatRoomEnemyInfo[] combatRoomEnemyInfos = new CombatRoomEnemyInfo[]
    {
        new CombatRoomEnemyInfo( new int[3] { 0, 1, -1 }, new int[2] { 2, 2 } ),
        new CombatRoomEnemyInfo( new int[3] { 0, 1, 2 }, new int[2] { 3, 3 } ),
        new CombatRoomEnemyInfo( new int[3] { -1, -1, -1 }, new int[2] { 3, 4 } ),
        new CombatRoomEnemyInfo( new int[3] { 0, 1, -1 }, new int[2] { 3, 4 } ),
        new CombatRoomEnemyInfo( new int[3] { 0, 1, 2 }, new int[2] { 4, 4 } ),
        new CombatRoomEnemyInfo( new int[3] { -1, -1, -1 }, new int[2] { 4, 5 } ),
        new CombatRoomEnemyInfo( new int[3] { 0, 1, -1 }, new int[2] { 4, 5 } ),
        new CombatRoomEnemyInfo( new int[3] { 0, 1, 2 }, new int[2] { 5, 5 } ),
        new CombatRoomEnemyInfo( new int[3] { -1, -1, -1 }, new int[2] { 5, 6 } )
    };

    


    private void Awake()
    {
        instance = this;
    }



    private void Start()
    {
        RoomSpawner();

        //for (int i = 0; i < allRoomCoordinates.Count; i++)
        //{
        //    StartCoroutine(LoadRoomRoutine());
        //}

        CreateRooms();
    }



    // Only adding the room coordinates to an array
    private void RoomSpawner()
    {
        int level = LevelManager.instance.Level;
        CombatRoomEnemyInfo combatRoomEnemyInfo = combatRoomEnemyInfos[level];

        // Creating start room
        allRoomTypes.Add(chestRoom);
        startNCombatRoomCoordinates.Add(Vector2.zero);
        allRoomCoordinates.Add(Vector2.zero);

        // Creating 3 combat rooms to exit room (Use for loop to generate consecutive rooms)
        for (int i = 0; i < 3; i++)
        {
            //GeneratePossibleRooms(i, 1, combatRoom, true);
            

            if (combatRoomEnemyInfo.GuaranteedEnemies[i] == -1)
            {
                GeneratePossibleRooms(i, 1, randomEnemyCombatRoom, true);
            }
            else
            {
                GeneratePossibleRooms(i, 1, fixedEnemyCombatRoom, true);
            }
        }

        // Creating exit room (TEST)
        GeneratePossibleRooms(3, 1, exitRoom, false);
        
        // Creating 3 more combat rooms
        for (int i = 0; i < 3; i++)
        {
            GeneratePossibleRooms(0, startNCombatRoomCoordinates.Count, randomEnemyCombatRoom, true);
        }

        // Creating 3 random treasure rooms
        for (int i = 0; i < 2; i++)
        {
            GeneratePossibleRooms(1, startNCombatRoomCoordinates.Count - 1, trappedChestRoom, false);
        }

        GeneratePossibleRooms(1, startNCombatRoomCoordinates.Count - 1, trappedChestRoom, false);
    }



    private void GeneratePossibleRooms(int firstRoomIndex, int noOfRoomsToCheck, GameObject roomType, bool isStartOrCombatRoom)
    {
        List<List<Vector2>> possibleRooms = new List<List<Vector2>>();

        for (int i = firstRoomIndex; i < firstRoomIndex + noOfRoomsToCheck; i++)
        {
            Vector2 originalRoomCoordinates = startNCombatRoomCoordinates[i];

            // Generate the four possible coordinates around a room, unless there is already a room in one of the coordinates
            for (int j = 0; j < 4; j++)
            {
                Vector2 neighbourCoordinates = originalRoomCoordinates + directions[j];
                if (!allRoomCoordinates.Contains(neighbourCoordinates))
                {
                    List<Vector2> neighbourInfoTemp = new List<Vector2>() { neighbourCoordinates, originalRoomCoordinates };
                    possibleRooms.Add(neighbourInfoTemp);
                }
            }
        }

        int randomNo = Random.Range(0, possibleRooms.Count);
        List<Vector2> neighbourInfo = possibleRooms[randomNo];

        Vector2 roomCoordinates = neighbourInfo[0];
        allRoomCoordinates.Add(roomCoordinates);
        allRoomTypes.Add(roomType);


        //  Door locations
        if (!doorLocations.ContainsKey(roomCoordinates))
        {
            List<Vector2> originalRoomCoordinates = new List<Vector2>() { neighbourInfo[1] };
            doorLocations.Add(roomCoordinates, originalRoomCoordinates);
        }
        else
        {
            doorLocations[roomCoordinates].Add(neighbourInfo[1]);
        }

        if (!doorLocations.ContainsKey(neighbourInfo[1]))
        {
            List<Vector2> roomCoordinatesList = new List<Vector2>() { roomCoordinates };
            doorLocations.Add(neighbourInfo[1], roomCoordinatesList);
        }
        else
        {
            doorLocations[neighbourInfo[1]].Add(roomCoordinates);
        }
        

        if (isStartOrCombatRoom)
        {
            startNCombatRoomCoordinates.Add(roomCoordinates);
        }
    }



    //IEnumerator LoadRoomRoutine()
    //{
    //    AsyncOperation loadRoom = SceneManager.LoadSceneAsync("Room", LoadSceneMode.Additive);

    //    while (loadRoom.isDone == false)
    //    {
    //        yield return null;
    //    }
    //}



    //private void RegisterRoom(Room room)
    //{
    //    room.roomCoordinates = allRoomCoordinates[0];
    //    room.transform.parent = this.transform;

    //    allRoomCoordinates.RemoveAt(0);
    //    allRoomTypes.RemoveAt(0);
    //}



    public void CreateRooms()
    {
        int fixedEnemyCounter = 0;

        for (int i = 0; i < allRoomCoordinates.Count; i++)
        {
            
            GameObject gameObject = Instantiate(allRoomTypes[i], this.transform, false) as GameObject;

            if (allRoomTypes[i] == fixedEnemyCombatRoom || allRoomTypes[i] == randomEnemyCombatRoom)
            {
                int[] totalPointRange = combatRoomEnemyInfos[LevelManager.instance.Level].TotalPointRange;
                int totalPoint = Random.Range(totalPointRange[0], totalPointRange[1] + 1);

                gameObject.GetComponent<NormalCombatRoom>().Level = LevelManager.instance.Level;
                if (fixedEnemyCounter <= 2)
                {
                    gameObject.GetComponent<NormalCombatRoom>().EnemyType = combatRoomEnemyInfos[LevelManager.instance.Level].GuaranteedEnemies[fixedEnemyCounter];
                    fixedEnemyCounter++;
                }
                else
                {
                    gameObject.GetComponent<NormalCombatRoom>().EnemyType = -1;
                }
                
                gameObject.GetComponent<NormalCombatRoom>().TotalPoint = totalPoint;
            }

            gameObject.transform.localPosition = new Vector3(allRoomCoordinates[i].x * width * 2, allRoomCoordinates[i].y * height * 2, 0);

            gameObject.GetComponent<Room>().roomCoordinates = allRoomCoordinates[i];
        }
    }
}