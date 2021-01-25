using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public class RoomInfo
//{
//    public string type;
//    public (int, int) coordinates;
//}

public class RoomControllerScrap : MonoBehaviour
{
    //public static RoomController instance;

    ////RoomInfo currentLoadRoomData;


    //public List<Room> loadedRooms = new List<Room>();

    //bool isLoadingRoom = false;


    //public int noOfCombatRooms;
    //private (int, int)[] directions = { (0, 1), (1, 0), (0, -1), (-1, 0) };

    //public List<(int, int)> roomCoordinates = new List<(int, int)>();

    //private List<string> roomTypes = new List<string>();



    //private void Awake()
    //{
    //    instance = this;

    //}

    //private void Start()
    //{
    //    for (int i = 0; i < nOfCombatRooms + 3; i++)
    //    {
    //        StartCoroutine(loadRoomRoutine());
    //    }

    //    //LoadRoom("Start", 0, 0);
    //    //LoadRoom("Start", 1, 0);
    //    //LoadRoom("Start", 0, 1);
    //    //LoadRoom("Start", -1, 0);
    //    //LoadRoom("Start", 0, -1);

    //    RoomSpawner();
    //}

    //private void Update()
    //{
    //    //UpdateRoomQueue();
    //}

    //void RoomSpawner()
    //{


    //    roomTypes.Add("Start");
    //    roomCoordinates.Add((0, 0));

    //    // Creating combat rooms
    //    for (int i = 0; i < 2; i++)
    //    {
    //        generatingPossibleRooms(i, 1, "Combat");

    //    }



    //    // Creating exit room (TEST)
    //    generatingPossibleRooms(2, 1, "Exit");



    //    // Creating special room
    //    generatingPossibleRooms(1, noOfCombatRooms, "Special");

    //    for (int i = 0; i < 5; i++)
    //    {
    //        Debug.Log(roomCoordinates[i] + roomTypes[i]);
    //    }


    //}



    //void generatingPossibleRooms(int firstRoomIndex, int noOfRoomsToCheck, string roomType)
    //{
    //    List<(int, int)> possibleRooms = new List<(int, int)>();


    //    for (int i = firstRoomIndex; i < firstRoomIndex + noOfRoomsToCheck; i++)
    //    {
    //        for (int j = 0; j < 4; j++)
    //        {
    //            int x = roomCoordinates[i].Item1 + directions[j].Item1;
    //            int y = roomCoordinates[i].Item2 + directions[j].Item2;

    //            if (!roomCoordinates.Contains((x, y)))
    //            {
    //                possibleRooms.Add((x, y));
    //            }
    //        }
    //    }

    //    System.Random random = new System.Random();
    //    int randomNo = random.Next(0, possibleRooms.Count);
    //    roomCoordinates.Add(possibleRooms[randomNo]);

    //    roomTypes.Add(roomType);
    //}



    ////void UpdateRoomQueue()
    ////{
    ////    if (!isLoadingRoom && loadRoomQueue.Count > 0)
    ////    {
    ////        currentLoadRoomData = loadRoomQueue.Dequeue();
    ////        isLoadingRoom = true;

    ////        StartCoroutine(loadRoomRoutine());
    ////    }
    ////}

    //public void LoadRoom(string name, int x, int y)
    //{
        //if (!DoesRoomExist(x, y))
        //{
        //    RoomInfo newRoomData = new RoomInfo();
        //    newRoomData.name = name;
        //    newRoomData.x = x;
        //    newRoomData.y = y;

        //    loadRoomQueue.Enqueue(newRoomData);
        //}

    //}

    //IEnumerator loadRoomRoutine()
    //{
    //    AsyncOperation loadRoom = SceneManager.LoadSceneAsync("Start", LoadSceneMode.Additive);

    //    while (loadRoom.isDone == false)
    //    {
    //        yield return null;
    //    }
    //}

    //public void RegisterRoom(Room room)
    //{
        //room.transform.position = new Vector3(currentLoadRoomData.x * room.width, currentLoadRoomData.y * room.height, 0);

        //room.x = currentLoadRoomData.x;
        //room.y = currentLoadRoomData.y;
        //room.transform.parent = transform;

        //isLoadingRoom = false;

        //loadedRooms.Add(room);
    //}

    //public bool DoesRoomExist(int x, int y)
    //{
    //    return loadedRooms.Find(item => item.x == x && item.y == y) != null;
    //}

}