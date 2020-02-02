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
    public Vector2 scaleX = new Vector2(1f, 1f), scaleY = new Vector2(1f, 1f), scaleZ = new Vector2(1f, 1f);

    [Range(0, 6)]
    public int spectrumBand;

    public float pingPongSpeed = 1f;

    public enum movType
    {
        spectrum,
        pingpong
    }

    [HideInInspector]
    public Vector3 originalPos;
}
