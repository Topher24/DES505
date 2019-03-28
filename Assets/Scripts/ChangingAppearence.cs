using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChangingAppearence : MonoBehaviourPunCallbacks, IPunObservable
{
	public bool gender;
    
	//public SpriteRenderer body;
    public Image part;
    public Sprite[] options;
    
    public int index;
	//public bool view;



    

    private void Update()
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i == index)
            {
                part.sprite = options[i];
                
            }
        }
        if(index != 0)
        {
            
        }

    }

	public void showPart(GameObject part){
		part.SetActive(gender);
       
	}

	public void bodyType()
	{
		if (gender == true) {
			gender = false; //Female
		} 
		else { 
			gender = true; //Male
		}
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