using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioClicker : AudioHandler
{
    MeshRenderer mRenderer;
    LerpScale lerpScale;

    [Header("Mario Screams")]
    public AudioClip[] screams;

    [Header("Materials")]
    public Material[] active;
    public Material[] inactive;

    Material origMat;

    public override void Awake()
    {
        base.Awake();

        mRenderer = GetComponent<MeshRenderer>();
        lerpScale = GetComponent<LerpScale>();
        origMat = mRenderer.material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (myAudioSource.isPlaying)
                myAudioSource.Stop();
        }   
    }

    void OnMouseEnter()
    {
        SetRandomMaterial(active);
    }

    void OnMouseOver()
    {

    }

    void OnMouseDown()
    {
        //stop...  
       
        PlayRandomSoundRandomPitch(screams, 1f);

        transform.localScale *= 2;

        lerpScale.SetScaler(3f, lerpScale.origScale);
    }

    void OnMouseExit()
    {
        SetRandomMaterial(inactive);
    }

    void SetRandomMaterial(Material[] mats)
    {
        int randomMat = Random.Range(0, mats.Length);
        mRenderer.material = mats[randomMat];
    }
}
