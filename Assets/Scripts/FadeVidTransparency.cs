using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FadeVidTransparency : MonoBehaviour
{
    public VideoPlayer vidPlayer;
    public bool fading;
    public float fadeSpeed;
    public float fadeTo = 0.01f;
    public SceneUtils scene;
    public Rick.Paper glassPlane;

    void Awake()
    {
        vidPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        //glassPlane.gameObject.SetActive(false);
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

        //when video is over, crummble
        if(vidPlayer.frame >= (long)vidPlayer.frameCount - 2)
        {
            FindObjectOfType<TakeScreenshot>().TakeShot();
            glassPlane.gameObject.SetActive(true);
            glassPlane.Crumble();

            scene.LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
