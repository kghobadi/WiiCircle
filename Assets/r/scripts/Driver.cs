using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rick {

    public class Driver : MonoBehaviour
    {
        Paper paper;
        Nerves nerves;


        public float speed = 0f;
        public float threshold = 133f;
        Vector3 a, b;

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
                return m_hitting && paper.active;
            }
        }

        Vector3 origin;

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
            nerves = GetComponent<Nerves>();

            paper = FindObjectOfType<Paper>();

            a = b = Input.mousePosition;
            origin = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 mouse = Input.mousePosition;

            b = mouse;
            speed = (b - a).magnitude / Time.deltaTime;
            a = b;

            ray = camera.ScreenPointToRay(mouse);
            raycastHit = new RaycastHit();

            m_hitting = Physics.Raycast(ray, out raycastHit, rayDistance, rayMask.value);
            if(colliding){
                position = raycastHit.point;
                normal = raycastHit.normal;

                if(Input.GetMouseButton(0)) {
                    nerves.strength = Mathf.Clamp01(speed / threshold);

                    if(speed > threshold)
                        paper.Crumble();
                    else
                        Draw();
                }
                else{
                    brushTime = brushRefresh;
                    nerves.strength = 0f;
                }
            }
            else{
                position = camera.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, defaultDistance));
                normal = -camera.transform.forward;

                nerves.strength = 0f;
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

            SmoothPosition();
        }

        void SmoothPosition(){
            Vector3 target = origin + transform.TransformVector(nerves.offset);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3f);
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
