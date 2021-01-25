using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public class RoomInfo
//{
//    public string type;
//    public (int, int) coordinates;
//}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    public int noOfCombatRooms;
    private (int, int)[] directions = { (0, 1), (1, 0), (0, -1), (-1, 0) };

    public List<(int, int)> roomCoordinates = new List<(int, int)>();

    private List<string> roomTypes = new List<string>();



    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        RoomSpawner();

        for (int i = 0; i < noOfCombatRooms + 3; i++)
        {
            StartCoroutine(loadRoomRoutine());
        }

        
    }



    void RoomSpawner()
    {
        roomTypes.Add("Start");
        roomCoordinates.Add((0, 0));

        // Creating combat rooms
        for (int i = 0; i < 2; i++)
        {
            generatingPossibleRooms(i, 1, "Combat");
            
        }

        // Creating exit room (TEST)
        generatingPossibleRooms(2, 1, "Exit");

        // Creating special room
        generatingPossibleRooms(1, noOfCombatRooms, "Special");

        for (int i = 0; i < 5; i++)
        {
            Debug.Log(roomCoordinates[i] + roomTypes[i]);
        }
        

    }



    void generatingPossibleRooms(int firstRoomIndex, int noOfRoomsToCheck, string roomType)
    {
        List<(int, int)> possibleRooms = new List<(int, int)>();


        for (int i = firstRoomIndex; i < firstRoomIndex + noOfRoomsToCheck; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int x = roomCoordinates[i].Item1 + directions[j].Item1;
                int y = roomCoordinates[i].Item2 + directions[j].Item2;

                if (!roomCoordinates.Contains((x, y)))
                {
                    possibleRooms.Add((x, y));
                }
            }
        }

        System.Random random = new System.Random();
        int randomNo = random.Next(0, possibleRooms.Count);
        roomCoordinates.Add(possibleRooms[randomNo]);

        roomTypes.Add(roomType);
    }



    IEnumerator loadRoomRoutine()
    {
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync("Start", LoadSceneMode.Additive);
        
        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        room.transform.position = new Vector3(roomCoordinates[0].Item1 * 11, roomCoordinates[0].Item2 * 14, 0);

        room.x = roomCoordinates[0].Item1;
        room.y = roomCoordinates[0].Item2;
        room.type = roomTypes[0];
        room.transform.parent = transform;

        roomCoordinates.RemoveAt(0);
        roomTypes.RemoveAt(0);
    }
    
}