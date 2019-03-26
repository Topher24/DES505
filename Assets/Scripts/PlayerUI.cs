using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerUI : MonoBehaviour
{

    public Canvas canvas;
    public GameObject avatar;

    // Use this for initialization
    void Start()
    {
        Vector3 avatarPos = new Vector3(500, 250 , 0);

        if (PhotonNetwork.IsMasterClient)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;

            avatar.transform.position = avatarPos;
        }
    }
}