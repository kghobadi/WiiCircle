using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class HoverReset : MonoBehaviour
                         , IPointerEnterHandler
{
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource src;

    public void OnPointerEnter(PointerEventData dat){
        src.PlayOneShot(clip);
    }
}
