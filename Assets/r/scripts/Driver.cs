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

        bool m_hitting = false;
        public bool colliding {
            get{
                return m_hitting;
            }
        }

        new Camera camera;
            Ray ray;
            RaycastHit raycastHit;
            [SerializeField] LayerMask rayMask;
            [SerializeField] float rayDistance = 100f, defaultDistance = 1f;

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

            m_hitting = Physics.Raycast(ray, out raycastHit, rayDistance, rayMask.value);
            if(colliding){
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
                paper.Draw(raycastHit.point);
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
