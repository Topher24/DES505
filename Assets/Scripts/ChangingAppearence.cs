using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingAppearence : MonoBehaviour
{
	public bool gender;
    
	//public SpriteRenderer body;
    public SpriteRenderer part;
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

    

    public void Swap()
    {
        if (index < options.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }

}