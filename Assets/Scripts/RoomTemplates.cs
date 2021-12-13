using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                    Vector2 spawnPos = new Vector2(rooms[i].transform.position.x, rooms[i].transform.position.y - 1);
                    Instantiate(drop, spawnPos, Quaternion.identity);
                    spawnedDrop = true;
                }
            }
        } else {
            waitTime -= Time.deltaTime;
        }   
    }

}
