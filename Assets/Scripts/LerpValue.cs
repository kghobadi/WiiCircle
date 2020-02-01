using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpValue : MonoBehaviour
{
    [Header("Lerp values")]
    [Tooltip("Check this to use linear movetowards instead of Lerp")]
    public bool moveTowardsOrLerp;
    [Tooltip("Check this to lerp on start")]
    public bool lerpOnStart;
    [Tooltip("True when lerping mat val")]
    public bool lerpingValue;
    [Tooltip("Start value = floatToLerp when Lerp is called")]
    public float startValue;
    [Tooltip("Value actively lerping to set mats float towards end value")]
    public float lerpValue;
    [Tooltip("Set publicly or when passed when Lerp is called")]
    public float endValue;
    [Tooltip("Speed of lerping value ")]
    public float lerpSpeed = 0.5f;
    
    //do we want to lerp on start?
    void Start()
    {
        if (lerpOnStart)
        {
            Lerp(startValue, endValue, lerpSpeed);
        }
    }

    //call to begin lerp 
    public void Lerp(float value, float desiredValue, float speed)
    {
        startValue = value;
        endValue = desiredValue;
        lerpSpeed = speed;
        
        lerpingValue = true;
    }

    void Update()
    {
        //lerp is under way!
        if (lerpingValue)
        {
            //move towards
            if (moveTowardsOrLerp)
            {
                //change mat Tp value 
                lerpValue = Mathf.MoveTowards(lerpValue, endValue, Time.deltaTime * lerpSpeed);
            }
            //lerp
            else
            {
                //lerp mat Tp value 
                lerpValue = Mathf.Lerp(lerpValue, endValue, Time.deltaTime * lerpSpeed);
            }
           
            //dist by abs value of lerpVal subtracted from end val
            float dist = Mathf.Abs(endValue - lerpValue);

            //close enough, let's finish im
            if (dist < 0.1f)
            {
                //stop lerping
                lerpingValue = false;
            }
        }

    }
}
