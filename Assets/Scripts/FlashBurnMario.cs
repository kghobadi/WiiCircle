using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBurnMario : AudioHandler
{
    [Header("Mario Screams")]
    public AudioClip[] screams;

    [Header("Materials")]
    public SkinnedMeshRenderer[] mRenderers;
    Material[] origMats;
    public Material dissolveBurn;
    LerpMaterial[] lerpMats;

    public GameObject videoPlayer;
    public Animator skeletonMario;
    public int burnFrameCount;

    public GameObject myCam, nextCam;
    public GameObject angel;
    public GameObject healthBar;

    public override void Awake()
    {
        base.Awake();
        
        //set orig mats & lerp mats
        origMats = new Material[mRenderers.Length];
        lerpMats = new LerpMaterial[mRenderers.Length];
        for (int i = 0; i < mRenderers.Length; i++)
        {
            origMats[i] = mRenderers[i].material;
            lerpMats[i] = mRenderers[i].GetComponent<LerpMaterial>();
        }
    }
    
    void Start()
    {
        myCam.SetActive(true);
        nextCam.SetActive(false);
        angel.SetActive(false);
        healthBar.SetActive(false);
        StartCoroutine(FlashBurnToSkeleton());
    }

    //does the cool flash mario effect 
    IEnumerator FlashBurnToSkeleton()
    {
        PlayRandomSoundRandomPitch(screams, 1f);

        for(int i = 0; i < burnFrameCount; i++)
        {
            //even
            if( i % 2 == 0)
            {
                //normal 
                SetMaterials(origMats);
            }
            //odd
            else
            {
                //dissolve burn 
                SetMaterial(dissolveBurn);
            }

            yield return new WaitForEndOfFrame();
        }

        videoPlayer.SetActive(true);
        PlayRandomSoundRandomPitch(screams, 1f);
        SetMaterial(dissolveBurn);

        //set dissolves
        for (int i = 0; i < lerpMats.Length; i++)
        {
            lerpMats[i].Lerp(mRenderers[i].material, 1f, 1f);
        }

        skeletonMario.enabled = true;
        myCam.SetActive(false);
        nextCam.SetActive(true);
        angel.SetActive(true);
        healthBar.SetActive(true);
    }
    
    //set all renderers to array of mats 
    void SetMaterials(Material[] mats)
    {
        for (int i = 0; i < mRenderers.Length; i++)
        {
            mRenderers[i].material = mats[i];
        }
    }

    //set all renderers to one mat
    void SetMaterial(Material mat)
    {
        for (int i = 0; i < mRenderers.Length; i++)
        {
            mRenderers[i].material = mat;
        }
    }
}
