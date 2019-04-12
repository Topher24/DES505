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
    //public bool view;



    public override void OnEnable()
    {
        gender = GetBool("Gender");
        
    }

    void Start()
    {
   

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


    }



    private void Update()
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

        if (!gendered)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].image.sprite = options[i];
                buttons[i].image.preserveAspect = true;
             
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].image.sprite == null)
            {
                buttons[i].interactable = false;
            }
        }
        if (!PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (i == index)
                {
                    part.sprite = options[i];

                }
            }
        }
    }

    public void ResetPart(Sprite sprite)
    {
        part.sprite = sprite;
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


public void showPart(GameObject part){
		part.SetActive(gender);
       
	}

	public void bodyType(bool ans)
	{
        
        gender = ans;
        SetBool("Gender", ans);


    }


    public void Swap(int i)
    {
        index = i;
        part.sprite = options[i];
       
        //if (index < options.Length - 1)
        //{
        //    index++;
        //}
        //else
        //{
        //    index = 0;
        //}
    }

    //void setPart(int i)
    //{
    //    PhotonView photonView = PhotonView.Get(this);
    //    photonView.RPC("Swap", RpcTarget.All, i);
    //}
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

    #endregion
}