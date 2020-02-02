using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rick {

    public class Paper : MonoBehaviour
    {
        [SerializeField]
        Texture2D canvas;

        [SerializeField]
        Material material;

        public float scaleX = 1f, scaleY = 1f;

        public Texture2D current {
            get{
                return canvas;
            }
        }

        int width = 640;
        int height = 640;

        public enum Blend { Multiply, Add };

        Fragment[] fragments;

        // Start is called before the first frame update
        void Start()
        {
            fragments = GetComponentsInChildren<Fragment>();
        }

        public Vector2 GetPositionOnPaper(Vector3 point){
            Vector3 local = transform.InverseTransformPoint(point);
            return new Vector2(local.x, local.z);
        }

        public void Crumble(){
            foreach(Fragment fr in fragments)
                fr.active = false;
        }

        public void Assemble(){
            foreach(Fragment fr in fragments)
                fr.active = true;
        }
    }

}
