using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;
    public Dropdown langPref;
    public Button playButton;

    #region Photon Callbacks

    //called when the local player left the room. We need to load the launchr scene

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}");
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, this.playerPrefab.transform.position, Quaternion.identity, 0);
        }
    }

    public void SetLanguage()
    {
        string lang = "";
        
        if (langPref.value == 0)
        {
            playButton.interactable = false;
        }
        else if(langPref.value == 1)
        {
            lang = "English";
            playButton.interactable = true;

        }
        else if(langPref.value == 2)
        {
            lang = "Mandarin";
            playButton.interactable = true;
        }

        PlayerPrefs.SetString(PhotonNetwork.NickName, lang);
    }
    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void LoadStart()
    {
        SceneManager.LoadScene("Start Screen", LoadSceneMode.Single);
    }

    #endregion

    #region private methods
    void LoadArena(string levelName)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level with {0} players.", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel(levelName);
    }

    #endregion

    #region Photon Callbacks

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena("AvatarCreation");
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena("AvatarCreation");
        }
    }




    
    #endregion
}

