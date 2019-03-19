using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderChanginScript : MonoBehaviour {

	public SpriteRenderer[] part;
	public Sprite[] options;
	public int index;


	private void Update()
	{
		//for (int j = 0; j < part.Length; j++) {

			for (int i = 0; i < options.Length; i++)
			{
				if (i == index)
				{
					part[i].sprite = options[i];
				}
			}
		//}
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
