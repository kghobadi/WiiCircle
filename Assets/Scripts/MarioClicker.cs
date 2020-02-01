using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MarioClicker : AudioHandler
{
    [Header("Mario Screams")]
    public AudioClip[] screams;

    [Header("Materials")]
    public SkinnedMeshRenderer[] mRenderers;
    Material[] origMats;
    LerpScale lerpScale;
    public Material[] active;
    public Material[] inactive;

    [Header("Cinemachine ref")]
    public CinemachineVirtualCamera cCamera;
    Camera mainCam;
    //transposer composer
    CinemachineTransposer transposer;
    CinemachineComposer composer;
    Vector3 origFollow, origAim;
    //noise
    CinemachineBasicMultiChannelPerlin cNoise;
    float origAmp, origFreq;

    public override void Awake()
    {
        base.Awake();
        
        lerpScale = GetComponent<LerpScale>();

        //set orig mats
        origMats = new Material[mRenderers.Length]; 
        for(int i = 0; i < mRenderers.Length; i++)
        {
            origMats[i] = mRenderers[i].material;
        }

        //set orig cmanchine values
        mainCam = Camera.main;

        if (cCamera)
        {
            transposer = cCamera.GetCinemachineComponent<CinemachineTransposer>();
            composer = cCamera.GetCinemachineComponent<CinemachineComposer>();
            origFollow = transposer.m_FollowOffset;
            origAim = composer.m_TrackedObjectOffset;

            //noise
            cNoise = cCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            origAmp = cNoise.m_AmplitudeGain;
            origFreq = cNoise.m_FrequencyGain;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
        }   
    }

    void Reset()
    {
        //reset audio
        if (myAudioSource.isPlaying)
            myAudioSource.Stop();

        //reset mats 
        for (int i = 0; i < mRenderers.Length; i++)
        {
            mRenderers[i].material = origMats[i];
        }

        //reset c machine 
        if (cCamera)
        {
            cNoise.m_AmplitudeGain = origAmp;
            cNoise.m_FrequencyGain = origFreq;
            transposer.m_FollowOffset = origFollow;
            composer.m_TrackedObjectOffset = origAim;
            // for 1 sec
            mainCam.clearFlags = CameraClearFlags.Color;
        }
    }

    void OnMouseEnter()
    {
        SetRandomMaterials(active);
        // for 1 sec
        mainCam.clearFlags = CameraClearFlags.Nothing;
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

        //cMachine fuckup
        if (cCamera)
        {
            cNoise.m_AmplitudeGain += 1;
            cNoise.m_FrequencyGain += 1;

            //transposer
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            transposer.m_FollowOffset += new Vector3(randomX, randomY, randomZ);

            //composer
            composer.m_TrackedObjectOffset -= new Vector3(randomX, randomY, randomZ);
        }
    }

    void OnMouseExit()
    {
        SetRandomMaterials(inactive);
    }

    void SetRandomMaterials(Material[] mats)
    {
        for(int i = 0; i < mRenderers.Length;i++)
        {
            int randomMat = Random.Range(0, mats.Length);
            mRenderers[i].material = mats[randomMat];
        }
    }

    void SetRandomMaterial(MeshRenderer mRenderer, Material[] mats)
    {
        int randomMat = Random.Range(0, mats.Length);
        mRenderer.material = mats[randomMat];
    }

    void OnDisable()
    {
        Reset();
    }
}
