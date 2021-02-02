using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    public int noOfCombatRooms;
    public int width;
    public int height;

    private Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    public List<Vector2> roomCoordinates = new List<Vector2>();
    public List<string> roomTypes = new List<string>();

    public Dictionary<Vector2, List<Vector2>> doorLocations = new Dictionary<Vector2, List<Vector2>>();
    private Vector2 roomToExitRoom;
    
    public Vector2 currentRoomCoordinates = new Vector2(0, 0);

    

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

        GenerateDoorLocations();
    }



    private void RoomSpawner()
    {
        roomTypes.Add("Start");
        roomCoordinates.Add(Vector2.zero);

        // Creating 2 combat rooms to exit room
        for (int i = 0; i < 2; i++)
        {
            GeneratePossibleRooms(i, 1, "Combat");
            
        }

        roomToExitRoom = roomCoordinates[2];

        // Creating exit room (TEST)
        GeneratePossibleRooms(2, 1, "Exit");
        

        // Creating more combat rooms

        // Creating special room
        GeneratePossibleRooms(1, noOfCombatRooms, "Special");

        //for (int i = 0; i < 5; i++)
        //{
        //    Debug.Log(roomCoordinates[i] + roomTypes[i]);
        //}


    }



    private void GenerateDoorLocations()
    {
        for (int i = 0; i < roomCoordinates.Count; i++)
        {
            List<Vector2> neighbours = new List<Vector2>();

            for (int j = 0; j < 4; j++)
            {
                Vector2 neighbourCoordinates = roomCoordinates[i] + directions[j];

                if (roomCoordinates.Contains(neighbourCoordinates))
                {
                    neighbours.Add(neighbourCoordinates);
                }
            }
            doorLocations.Add(roomCoordinates[i], neighbours);
        }

        //  Remove doors to exit room
        Vector2 exitRoomCoordinates = roomCoordinates[roomTypes.IndexOf("Exit")];

        foreach (var room in doorLocations[exitRoomCoordinates])
        {
            if (room != roomToExitRoom) {
                doorLocations[room].Remove(exitRoomCoordinates);
            }
        }

        doorLocations[exitRoomCoordinates] = new List<Vector2>() {roomToExitRoom};

        //  Remove doors from start to special room (TEST IT!!!)
        Vector2 specialRoomCoordinates = roomCoordinates[roomTypes.IndexOf("Special")];
        if (specialRoomCoordinates.magnitude == 1)
        {
            doorLocations[Vector2.zero].Remove(specialRoomCoordinates);
            doorLocations[specialRoomCoordinates].Remove(Vector2.zero);
        }

    }



    private void GeneratePossibleRooms(int firstRoomIndex, int noOfRoomsToCheck, string roomType)
    {
        List<Vector2> possibleRooms = new List<Vector2>();


        for (int i = firstRoomIndex; i < firstRoomIndex + noOfRoomsToCheck; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector2 neighbourCoordinates = roomCoordinates[i] + directions[j];
                if (!roomCoordinates.Contains(neighbourCoordinates))
                {
                    possibleRooms.Add(neighbourCoordinates);
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
        room.transform.position = new Vector3(roomCoordinates[0].x * width * 2, roomCoordinates[0].y * height * 2, 0);

        room.coordinates = roomCoordinates[0];
        room.type = roomTypes[0];
        room.transform.parent = transform;

        roomCoordinates.RemoveAt(0);
        roomTypes.RemoveAt(0);
    }
    
}