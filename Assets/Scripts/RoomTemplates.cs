using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    
    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedDrop;
    public GameObject drop;

    void Update() {
        if (waitTime <= 0 && spawnedDrop == false) {
            for (int i = 0; i < rooms.Count; i++) {
                if (i == rooms.Count - 1) {
                    Instantiate(drop, rooms[i].transform.position, Quaternion.identity);
                    spawnedDrop = true;
                }
            }
        } else {
            waitTime -= Time.deltaTime;
        }   
    }

}
