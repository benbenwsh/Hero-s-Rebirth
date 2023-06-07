using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private void Update()
    {
        RoomController roomController = RoomController.instance;
        Vector3 newCameraPosition = new Vector3(roomController.currentRoomCoordinates.x * roomController.width * 2 + 0.5f, roomController.currentRoomCoordinates.y * roomController.height * 2 + 1, -10);
        transform.position = Vector3.MoveTowards(transform.position, newCameraPosition, Time.deltaTime * 100);
    }


}
