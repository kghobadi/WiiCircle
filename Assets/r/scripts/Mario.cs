using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rick {

    [RequireComponent(typeof(Animator))]
    public class Mario : MonoBehaviour
    {
        Driver driver;
        Animator animator;

        public bool active = true;

        [Range(0f, 1f)] public float lookAtWeight = 1f;
        [SerializeField] float distanceFromPage = .1f;

        void Awake() {
            animator = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {
            driver = FindObjectOfType<Driver>();
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
    }

}
