using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rick {

    public class Driver : MonoBehaviour
    {
        
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


        public bool debug = true;
                GameObject debugObject = null;
        
        // Start is called before the first frame update
        void Start()
        {
            camera = GetComponent<Camera>();
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
            }
            else{
                position = camera.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, defaultDistance));
                normal = -camera.transform.forward;
            }


            if(debug){
                EnsureDebug();
                debugObject.transform.position = position;
            }
            else
                ClearDebug();

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
