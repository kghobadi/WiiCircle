using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FadeVidTransparency : MonoBehaviour
{
    public VideoPlayer vidPlayer;
    public bool fading;
    public float fadeSpeed;
    public float fadeTo = 0.01f;

    void Awake()
    {
        vidPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        SetFade();
    }

    void SetFade()
    {
        fading = true;
    }

    void Update()
    {
        if (fading)
        {
            vidPlayer.targetCameraAlpha = Mathf.Lerp(vidPlayer.targetCameraAlpha, fadeTo, Time.deltaTime * fadeSpeed);
            //stop fading
            if(Mathf.Abs(fadeTo - vidPlayer.targetCameraAlpha)  < 0.001f)
            {
                fading = false;
            }
        }
    }
}
