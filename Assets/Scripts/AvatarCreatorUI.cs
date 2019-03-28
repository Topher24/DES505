using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarCreatorUI : MonoBehaviour {

    public GameObject panel1;
    public GameObject panel2;

    public void SwitchPanel()
    {
        panel1.SetActive(false);

        panel2.SetActive(true);
    }
}
