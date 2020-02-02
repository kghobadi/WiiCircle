using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rick {

    public class Manager : MonoBehaviour
    {
        Paper paper;

        [SerializeField] SimpleTransition transitionOut;

        [SerializeField] RenderTexture canvas;
        [SerializeField] UnityEngine.UI.RawImage finalImage;

        [SerializeField] float timeBetweenReset = 1f;

        int deaths = 0;
        int maxDeaths = 5;

        bool ending = false;

        // Start is called before the first frame update
        void Start()
        {
            paper = FindObjectOfType<Paper>();
            paper.onCrumbled += Restart;
        }

        // Update is called once per frame
        void Update()
        {
            if(ending)
                return;

            if(deaths >= maxDeaths){
                transitionOut.TransitionOut();
                ending = true;
            }
        }

        void Restart(){
            ++deaths;
            if(!ending)
                PassTexture();

            StartCoroutine("Respawn");
        }

        void PassTexture(){
            Texture2D final = new Texture2D(640, 640, TextureFormat.ARGB32, true, true);

            var previousActive = RenderTexture.active;

            RenderTexture.active = canvas;

            final.ReadPixels(new Rect(0f, 0f, canvas.width, canvas.height), 0, 0, true);
            final.Apply();

            RenderTexture.active = previousActive;
            finalImage.texture = final;
        }

        IEnumerator Respawn(){
            yield return new WaitForSeconds(timeBetweenReset);
            paper.Assemble();
        }

        void OnDestroy() {
            paper.onCrumbled -= Restart;    
        }
    }

}
