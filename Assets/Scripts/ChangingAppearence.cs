using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChangingAppearence : MonoBehaviourPunCallbacks, IPunObservable
{
    bool gender;
    public bool gendered;
    public Button[] buttons;
    //public SpriteRenderer body;
    public Image part;
    Sprite[] options = new Sprite[6];
    public Sprite[] fOptions;
    public Sprite[] mOptions;

    int index;
    public Sprite empty;

    //public int index;

  





    public override void OnEnable()

    {
        /////////////////////////////////////
        //////this gets called too much//////
        /////////////////////////////////////

        //if (PhotonNetwork.IsMasterClient)
        gender = GetBool("Gender");
     
        
    }





    private void Update()
    {

        ChangeOptions();

        if (!gendered)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].image.sprite = options[i];
            }
        }

       

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].image.sprite == null)
            {
                buttons[i].gameObject.SetActive(false);
            }
            else
            {
                buttons[i].gameObject.SetActive(true);
            }
        }

        

    }

    [PunRPC]
    public void ResetPart()
    {
        part.sprite = empty;
       
            
    }
        
     


    
    

    static void SetBool(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }


    static bool GetBool(string key)
    {
        int value = PlayerPrefs.GetInt(key);

        if (value == 1)
        {
            return true;
        }

        else
        {
            return false;
        }

        
    }

    void ChangeOptions()
    {
        if (!gender)
        {
            for (int i = 0; i < fOptions.Length; i++)
            {
                options[i] = fOptions[i];
            }
        }
        else
        {
            for (int i = 0; i < mOptions.Length; i++)
            {
                options[i] = mOptions[i];
            }
        }

    }

    [PunRPC]
    public void bodyType(bool ans)
	{
        
        gender = ans;
        SetBool("Gender", ans);

        for (int i = 0; i < options.Length; i++)
        {
            options[i] = null;
        }

       
    }

    [PunRPC]

    public void Swap(int j)
    {
        index = j;

        for (int i = 0; i < options.Length; i++)
        {
            if (i == index)
            {
                part.sprite = options[i];

            }
        }
    }


    //RPC Functions

    public void RPCReset()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ResetPart", RpcTarget.All);
    }


    public void setGender(bool ans)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("bodyType", RpcTarget.All, ans);
    }


    public  void setPart(int i)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Swap", RpcTarget.All, i);
    }



    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(index);
            stream.SendNext(gender);
        }
        else
        {
            this.index = (int)stream.ReceiveNext();
            this.gender = (bool)stream.ReceiveNext();
        }
    }

    public void Confirm()
    {


        string key = PhotonNetwork.NickName + " " + part.transform.name;
        Debug.Log("Saved: " + key);
        PlayerPrefs.SetInt(key, index);
        
    }

    #endregion
}