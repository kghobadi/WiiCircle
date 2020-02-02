using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudioWithTransition : MonoBehaviour
{
    [SerializeField] SimpleTransition transition;

    [SerializeField] float maxVolume = 1f;
    [SerializeField] bool inverted = true;

    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(transition != null)
            transition.onAlphaChanged += SetVolume;

    }

    void SetVolume(float value){
        if(inverted)
            audioSource.volume = maxVolume*(1f - value);
        else
            audioSource.volume = maxVolume*value;
    }

    private void OnDestroy() {
        if(transition != null)
            transition.onAlphaChanged -= SetVolume;
    }
}
