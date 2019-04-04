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
    public int index;
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

            }
        }
        
        for (int i = 0; i < options.Length; i++)
        {
            if (i == index)
            {
                part.sprite = options[i];
                
            }
        }

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

       
        //if (index < options.Length - 1)
        //{
        //    index++;
        //}
        //else
        //{
        //    index = 0;
        //}
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

    #endregion
}