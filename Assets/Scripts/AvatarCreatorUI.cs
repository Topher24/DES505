using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class AvatarCreatorUI : MonoBehaviour {

    public GameObject panel1;
    public GameObject panel2;

    
    public int round = 0; // 0 = english 1 = mandarin





    [PunRPC]
    public void SwitchPanel()
    {
        panel1.SetActive(false);

        panel2.SetActive(true);

    }


    void UITitles(string trueString, string falseString)
    {
        GameObject[] titles;
        if (round == 0)
        {
            titles = GameObject.FindGameObjectsWithTag(trueString);
            for (int i = 0; i < titles.Length; i++)
            {
                titles[i].SetActive(true);
            }

            for (int i = 0; i < titles.Length; i++)
            {
                titles[i] = null;
            }

            titles = GameObject.FindGameObjectsWithTag(falseString);
            for (int i = 0; i < titles.Length; i++)
            {
                titles[i].SetActive(false);
            }
        }
    }
    
    

    private void Start()
    {
        Button[] buttons = this.GetComponentsInChildren<Button>();
        
            if (PhotonNetwork.IsMasterClient)
            {
                for (int i = 0; i < buttons.Length; i++)
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


        if (getString(PhotonNetwork.NickName) == "Mandarin")
        {
            UITitles("English", "Mandarin");
        }
        else if (getString(PhotonNetwork.NickName) == "English")
        {
            UITitles("Mandarin", "English");
        }

        Debug.Log( "Player Prefs: " + getString(PhotonNetwork.NickName));

        GameObject[] masterComps;

        masterComps = GameObject.FindGameObjectsWithTag("Master");


        if (!PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < masterComps.Length; i++)
            {
                masterComps[i].SetActive(false);
            }
        }

        GameObject[] clientComps;

        clientComps = GameObject.FindGameObjectsWithTag("Client");


        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < clientComps.Length; i++)
            {
               // clientComps[i].SetActive(false);
            }
        }



    }

    string getString(string key)
    {
        return PlayerPrefs.GetString(key);
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
