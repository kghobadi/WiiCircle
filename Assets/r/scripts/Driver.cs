using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rick {

    public class Driver : MonoBehaviour
    {
        Paper paper;


        Vector3 m_position;
        public Vector3 position {
            get{
                return m_position;
            }
            set{
                m_position = value;
            }
        }

        Vector3 m_normal;
        public Vector3 normal {
            get{
                return m_normal;
            }
            set{
                m_normal = value;
            }
        }

        new Camera camera;
            Ray ray;
            RaycastHit raycastHit;
            [SerializeField] LayerMask rayMask;
            [SerializeField] float rayDistance = 100f, defaultDistance = 1f;

        [SerializeField] Camera drawCamera;
        [SerializeField] GameObject drawBrush, brushContainer;
                         List<GameObject> brushes = new List<GameObject>();

        [Range(0f, 1f)]
        public float brushSize = 1f;
        public float brushRefresh = .1f;
               float brushTime = 0f;


        public bool debug = true;
                GameObject debugObject = null;
        
        // Start is called before the first frame update
        void Start()
        {
            camera = GetComponent<Camera>();
            paper = FindObjectOfType<Paper>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 mouse = Input.mousePosition;

            ray = camera.ScreenPointToRay(mouse);
            raycastHit = new RaycastHit();

            bool hitting = Physics.Raycast(ray, out raycastHit, rayDistance, rayMask.value);
            if(hitting){
                position = raycastHit.point;
                normal = raycastHit.normal;

                if(Input.GetMouseButton(0))
                    Draw();
                else
                    brushTime = brushRefresh;
            }
            else{
                position = camera.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, defaultDistance));
                normal = -camera.transform.forward;
            }

            if(Input.GetKeyDown(KeyCode.A))
                paper.Assemble();
            else if(Input.GetKeyDown(KeyCode.D))
                paper.Crumble();


            if(debug){
                EnsureDebug();
                debugObject.transform.position = position;
            }
            else
                ClearDebug();

        }

        void Draw(){

            if(brushTime >= brushRefresh){
                Vector2 position = paper.GetPositionOnPaper(raycastHit.point);
                Vector2 normal_position = ((position * 5f) + Vector2.one) / 2f;
                    Debug.Log(normal_position);

                var pos = drawCamera.ViewportToWorldPoint(new Vector3(normal_position.x, normal_position.y, 100f));

                var b = Instantiate(drawBrush, pos, drawBrush.transform.rotation, brushContainer.transform);
                    b.transform.localScale = Vector3.one * brushSize;
                    brushes.Add(b);

                brushTime = 0f;
            }
            else
                brushTime += Time.deltaTime;
        }

        void EnsureDebug(){
            if(debugObject == null){
                debugObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                debugObject.transform.localScale = Vector3.one * .33f;
            }
        }

        void ClearDebug(){
            if(debugObject != null)
                GameObject.Destroy(debugObject);
        }
    }

}
