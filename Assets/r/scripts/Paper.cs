using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rick {

    public class Paper : MonoBehaviour
    {
        public delegate void OnCrumbled();
        public event OnCrumbled onCrumbled;

        Fragment[] fragments;


        [SerializeField] new Camera camera;
        [SerializeField] RenderTexture texture;

        [SerializeField] GameObject drawBrush, brushContainer;
        [SerializeField] List<GameObject> brushes = new List<GameObject>();

        [Range(0f, 1f)]
        public float brushSize = 1f;

        public bool active = true;


        // Start is called before the first frame update
        void Start()
        {
            fragments = GetComponentsInChildren<Fragment>();
        }

        public Vector2 GetPositionOnPaper(Vector3 point){
            Vector3 local = transform.InverseTransformPoint(point);
            return new Vector2(local.x, local.z);
        }

        public void Draw(Vector3 p){
            Vector2 position = GetPositionOnPaper(p);
            Vector2 normal_position = ((position * 5f) + Vector2.one) / 2f;

            var pos = camera.ViewportToWorldPoint(new Vector3(normal_position.x, normal_position.y, 100f));

            var b = Instantiate(drawBrush, pos, drawBrush.transform.rotation, brushContainer.transform);
                b.transform.localScale = Vector3.one * brushSize;
                brushes.Add(b);
        }

        void Erase(){
            if(brushes != null && brushes.Count > 0){            
                GameObject[] activeBrushes = brushes.ToArray();
                foreach(GameObject b in activeBrushes)
                    GameObject.Destroy(b);
            }

            brushes = new List<GameObject>();
            texture.Release();
        }

        public void Crumble(){
            foreach(Fragment fr in fragments)
                fr.active = false;

            active = false;

            if(onCrumbled != null)
                onCrumbled();
                
            Erase();
        }

        public void Assemble(){
            foreach(Fragment fr in fragments)
                fr.active = true;

            active = true;  
        }
    }

}
