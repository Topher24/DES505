using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChangingColourScript : MonoBehaviourPunCallbacks, IPunObservable
{ 

    public GameObject panel;
    public SpriteRenderer shirt;
    public Image squareShirtDisplay;
    public Color[] colors;
    public int whatColor = 1;


    private void Update()
    {

        squareShirtDisplay.color = shirt.color;

        for(int i = 0; i < colors.Length; i++)
        {
            if(i == whatColor)
            {
                shirt.color = colors[i];
            }
        }
    }

   public void ChangePanelState(bool state)
    {
        panel.SetActive(state);
    }
	

  public void ChangeShirtColor(int index)
    {
        whatColor = index;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(whatColor);

        }
        else
        {
            this.whatColor = (int)stream.ReceiveNext();
        }
        
    }
}
