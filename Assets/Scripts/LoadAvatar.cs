using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoadAvatar : MonoBehaviour {

    public int head;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey(PhotonNetwork.NickName + "head"))
        {
            head = PlayerPrefs.GetInt(PhotonNetwork.NickName + "head");
        }
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
