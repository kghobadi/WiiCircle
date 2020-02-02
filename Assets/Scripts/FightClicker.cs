using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightClicker : MonoBehaviour
{
    public FightingAnimations myAnimations;

    void OnMouseDown()
    {
        Debug.Log("click");
        myAnimations.TriggerRandomAnim(myAnimations.fightTriggers);
    }
}
