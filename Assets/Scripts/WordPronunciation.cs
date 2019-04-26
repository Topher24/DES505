using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WordPronunciation : MonoBehaviour, IPointerDownHandler
{

    AudioSource word;

    void Start()
    {
        word = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        word.Play();
    }
}

