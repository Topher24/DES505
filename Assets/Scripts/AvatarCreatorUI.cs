using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class AvatarCreatorUI : MonoBehaviour {

    public GameObject panel1;
    public GameObject panel2;


    [PunRPC]
    public void SwitchPanel()
    {
        panel1.SetActive(false);

        panel2.SetActive(true);
    }

    public void SetActive(GameObject thing, bool active)
    {
        thing.SetActive(active);
    }

    public void RPCSwitchPanel()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SwitchPanel", RpcTarget.All);
    }
    
}
