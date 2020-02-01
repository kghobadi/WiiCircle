using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDance : MonoBehaviour
{

    public AudioSpectrum spectrum;

    public Transform[] joints;
    DanceJoint[] danceJoints;

    public float levelScale, initScale;

    public bool visualizer;
    // Use this for initialization
    void Start()
    {
        danceJoints = FindObjectsOfType<DanceJoint>();

        foreach (DanceJoint d in danceJoints)
        {
            d.originalPos = d.transform.localPosition;
        }

    }

    float maxNum = 0;
    // Update is called once per frame
    void Update()
    {
        //can only have as many joints as bands (10)!
        //if (visualizer)
        //{
        //for (int i = 0; i < joints.Length; i++)
        //{
        //   joints[i].localScale = Vector3.one * (levelScale * spectrum.MeanLevels[i % spectrum.MeanLevels.Length] + initScale);
        //    //            Debug.Log(i + "|||" + spectrum.MeanLevels[i]);
        //}
        //}

        foreach (DanceJoint d in danceJoints)
        {
            if (d.posMoveType == DanceJoint.movType.pingpong)
                d.transform.localPosition = d.originalPos + new Vector3(RemapRange(Mathf.Sin(Time.time), -1, 1, d.posX.x, d.posX.y),
                                                                        RemapRange(Mathf.Sin(Time.time), -1, 1, d.posY.x, d.posY.y),
                                                                        RemapRange(Mathf.Sin(Time.time), -1, 1, d.posZ.x, d.posZ.y));
            else if (d.posMoveType == DanceJoint.movType.spectrum)
                d.transform.localPosition = d.originalPos + new Vector3(RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.posX.x, d.posX.y),
                                                                        RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.posY.x, d.posY.y),
                                                                        RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.posZ.x, d.posZ.y));

            if (d.rotMoveType == DanceJoint.movType.pingpong)
                d.transform.localEulerAngles = new Vector3(RemapRange(Mathf.Sin(Time.time), -1, 1, d.rotX.x, d.rotX.y),
                                                                           RemapRange(Mathf.Sin(Time.time), -1, 1, d.rotY.x, d.rotY.y),
                                                                           RemapRange(Mathf.Sin(Time.time), -1, 1, d.rotZ.x, d.rotZ.y));
            else if (d.rotMoveType == DanceJoint.movType.spectrum)
                d.transform.localEulerAngles = new Vector3(RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.rotX.x, d.rotX.y),
                                                           RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.rotY.x, d.rotY.y),
                                                           RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.rotZ.x, d.rotZ.y));

            if (d.scaleMoveType == DanceJoint.movType.pingpong)
                d.transform.localScale = new Vector3(RemapRange(Mathf.Sin(Time.time), -1, 1, d.scaleX.x, d.scaleX.y),
                                                     RemapRange(Mathf.Sin(Time.time), -1, 1, d.scaleY.x, d.scaleY.y),
                                                     RemapRange(Mathf.Sin(Time.time), -1, 1, d.scaleZ.x, d.scaleZ.y));
            else if (d.scaleMoveType == DanceJoint.movType.spectrum)
                d.transform.localScale = new Vector3(RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.scaleX.x, d.scaleX.y),
                                                     RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.scaleY.x, d.scaleY.y),
                                                     RemapRange(spectrum.MeanLevels[d.spectrumBand], 0, .1f, d.scaleZ.x, d.scaleZ.y));

        }

    }


    float RemapRange(float oldValue, float oldMin, float oldMax, float newMin, float newMax, bool clamped = true)
    {
        if (clamped)
        {
            float realOldMax = Mathf.Max(oldMin, oldMax);
            float realOldMin = Mathf.Min(oldMin, oldMax);
            oldValue = Mathf.Clamp(oldValue, realOldMin, realOldMax);
        }

        float newValue = 0;
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
        return newValue;
    }
}
