using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

public class SimpleTransition : MonoBehaviour
{
    public UnityEvent OnEnter, OnExit;

    [SerializeField] CanvasGroup group;

    float alpha = 0f, target = 0f;
    bool transitioning = false;

    float transitionTime = 0f, transition_t = 0f;
    float direction = 0f;

    [SerializeField] bool onawake = false, overrideInteractable = true;
    [SerializeField] float defaultTransitionIn = 1f, defaultTransitionOut = 1f;

    private void Awake() {
        if(onawake)
            TransitionIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(transitioning == false)
            return;

        bool flag = false;

        float dist = Mathf.Abs(target - alpha);
        if(dist > .001f) {
            alpha = Mathf.Lerp(alpha, target, Mathf.Clamp01(transition_t / transitionTime));
            transition_t += Time.deltaTime;
        }
        else {
            alpha = target;
            flag = true;

            if(direction == 1f)
                OnEnter.Invoke();
            if(direction == -1f)
                OnExit.Invoke();
        }

        UpdateVisibility();
        if(flag) {
            transitioning = false;
            direction = 0f;
        }
    }

    void UpdateVisibility(){
        if(group == null)
            return;

        group.alpha = alpha;
    
        if(overrideInteractable){
            bool interactable = (alpha > 0f);
                group.blocksRaycasts = interactable;
                group.interactable = interactable;
        }
    }

    public void TransitionIn(){
        TransitionIn(defaultTransitionIn, true);
    }

    public void TransitionIn(float time = 1f, bool reset = false){
        if(group == null)
            return;

        if(reset)
            alpha = 1f;

        target = 0f;
        direction = 1f;
        Reset(time);
    }
    
    public void TransitionOut(){
        TransitionOut(defaultTransitionOut, true);
    }

    public void TransitionOut(float time = 1f, bool reset = false){
        if(group == null)
            return;

        if(reset)
            alpha = 0f;

        target = 1f;
        direction = -1f;
        Reset(time);
    }

    void Reset(float t = 1f){
        transitionTime = t;
        transition_t = 0f;
        transitioning = true;
    }
}
