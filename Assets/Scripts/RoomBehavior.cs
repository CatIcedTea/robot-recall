using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls; //0-up 1-down 2-right 3-left
    public GameObject[] doors;
    public bool[] betaStatus;
    void Start()
    {
        UpdateRoom(betaStatus);
    }
    public void UpdateRoom(bool[] status)
    {
        for(int i = 0; i<status.Length; i++){
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
