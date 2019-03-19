using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks {
    #region Private Serializable Fields

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 2;
    #endregion

    #region Private Fields
    //This client's version number. User are separated from each other by gameVersion (which allows you to make breaking changes)
    string gameVersion = "1";

    /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
    /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    bool isConnecting;
    #endregion

    #region MonoBehaviour CallBacks

    //MonoBehaviour method called on GameObject by UNity during early init phase

    private void Awake()
    {
        //#Critical
        //This makes sure we can use PhotonNerwork.LoadLevel() on the master client and all client in the same room sync their level automatically

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    #endregion

    #region Public Methods


    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network

    public void Connect()
    {

        isConnecting = true;
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        //We check if we are econnected or not, we join if we are, else we init the connect to the server
        if (PhotonNetwork.IsConnected)
        {
            //#Critical, we need at this point to attempt joining a random room. If it fails we'll get notified in OnJoinRandomFailed() and we'll create one
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //#Critical, we must first and formost connect to Photon Online Server
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    [Tooltip("The UI Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;
    #endregion

    #region MonoBehaviourPunCallbacks

    public override void OnConnectedToMaster()
    {
        // we don't want to do anything if we are not attempting to join a room.
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (isConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
        }

        Debug.Log("AvatarCreator/Launcher : OnConnectedToMaster() was called by PUN");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("AvatarCreator/Launcher: OnDisconneced() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {

        // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Menu' ");


            // #Critical
            // Load the Room Level.
            PhotonNetwork.LoadLevel("Menu");
        }

        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    #endregion
}
