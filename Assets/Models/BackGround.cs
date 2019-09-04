using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject sang, toi, myRoomSang, myRoomToi, hospital;

    public void TatToanBoBackGround()
    {
        sang.SetActive(false);
        toi.SetActive(false);
        hospital.SetActive(false);
        myRoomSang.SetActive(false);
        myRoomToi.SetActive(false);
    }
}
