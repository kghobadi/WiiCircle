using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rick {

    public class Nerves : MonoBehaviour
    {
        public Vector3 offset;

        [SerializeField] float frequency = 1f;
        [SerializeField] float magnitude = 1f;

        [SerializeField] AnimationCurve frequencyCurve, magnitudeCurve;

        float m_strength = 0f;
        public float strength {
            set{
                m_strength = Mathf.Clamp01(value);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("Shake");   
        }

        IEnumerator Shake(){
            while(true){
                float freq = (1f - frequencyCurve.Evaluate(m_strength)) * frequency;
                float mag = magnitudeCurve.Evaluate(m_strength) * magnitude;
                
                if(m_strength > 0f)
                    offset = Random.insideUnitSphere * Random.Range(0f, mag);
                else
                    offset = Vector3.zero;

                yield return new WaitForSeconds(freq);
            }
        }
    }

}
