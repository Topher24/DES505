using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ClientMasterUI : MonoBehaviour {

 
    public Button[] buttons;

	// Use this for initialization
	void Start () {
        
        
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
