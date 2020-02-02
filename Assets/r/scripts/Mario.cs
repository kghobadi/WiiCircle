using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rick {

    [RequireComponent(typeof(Animator))]
    public class Mario : MonoBehaviour
    {
        Driver driver;
        Animator animator;

        [SerializeField] GameObject pencil;
        [SerializeField] Vector3 pencilActiveOffset, pencilInactiveOffset;

        [SerializeField] Transform l_hand;

        public bool active = true;

        [Range(0f, 1f)] public float lookAtWeight = 1f;
        [SerializeField] float distanceFromPage = .1f;

                         AudioSource audioSource;
        [SerializeField] AudioClip onCrumble, onLose, onSnore;

        void Awake() {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {
            driver = FindObjectOfType<Driver>();
            
            Paper.onCrumbled += Crumbled;
            Manager.Lost += Lost;
        }

        void Update() {
            if(pencil != null && l_hand != null){
                if(driver.colliding){
                    pencil.transform.position = l_hand.position + l_hand.TransformDirection(pencilActiveOffset);
                    pencil.transform.up = -driver.normal;
                }
                else {
                    pencil.transform.position = l_hand.position + l_hand.TransformDirection(pencilInactiveOffset);
                    pencil.transform.up = l_hand.transform.right;
                }
            }    
        }

        void OnAnimatorIK(int index) {
            if(animator){
                if(active && driver != null){
                    animator.SetLookAtWeight(lookAtWeight);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);

                    animator.SetLookAtPosition(driver.position);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, driver.position + driver.normal * distanceFromPage);
                }
                else{
                    animator.SetLookAtWeight(0f);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                }
            }
        }

        void Crumbled(){
            if(stillCrumble)
                audioSource.PlayOneShot(onCrumble);
        }

        bool stillCrumble = true;

        void Lost(){
            stillCrumble = false;
            StartCoroutine("Losing");
        }

        public void Snore(){
            audioSource.loop = true;
            audioSource.clip = onSnore;
            audioSource.Play();
        }

        IEnumerator Losing(){
            yield return new WaitForSeconds(1.5f);
             audioSource.PlayOneShot(onLose);
        }

        private void OnDestroy() {
            Paper.onCrumbled -= Crumbled;
            Manager.Lost -= Lost;
        }
    
    }

}
