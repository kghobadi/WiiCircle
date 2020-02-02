using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{

    public Rick.Paper glassPlane;

    public Texture2D tex;

    private void Update()
    {
        
               
    }

    public void TakeShot()
    {
        StartCoroutine("TakingShot");
    }

    IEnumerator TakingShot()
    {
        yield return new WaitForEndOfFrame();

        var scrnsht = tex = new Texture2D(Screen.width, Screen.height);


        RenderTexture rt = new RenderTexture(scrnsht.width / 2, scrnsht.height / 2, 0);

        var cam = Camera.main;
        var prt = cam.targetTexture;

        cam.targetTexture = rt;
        cam.Render();

        scrnsht.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        scrnsht.Apply();

        cam.targetTexture = prt;

        Debug.Log("assigned");
    }
}
