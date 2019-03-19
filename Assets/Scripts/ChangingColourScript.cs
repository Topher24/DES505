using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingColourScript : MonoBehaviour {

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
}
