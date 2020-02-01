using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceJoint : MonoBehaviour
{
    public movType posMoveType;
    public Vector2 posX, posY, posZ;
    public movType rotMoveType;
    public Vector2 rotX, rotY, rotZ;
    public movType scaleMoveType;
    public Vector2 scaleX, scaleY, scaleZ;

    public bool randomSpectrumBand;
    [Range(0, 9)]
    public int spectrumBand;

    public enum movType
    {
        spectrum,
        pingpong
    }

    [HideInInspector]
    public Vector3 originalPos;
}
