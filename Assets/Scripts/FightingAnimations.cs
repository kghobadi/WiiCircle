using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingAnimations : AnimationHandler
{

    public string[] fightTriggers;
    Vector3 origPos;

    public Transform opponent;

    void Start()
    {
        origPos = transform.position;   
    }

    void Update()
    {
        transform.position = origPos;

        Vector3 lookAtPos = new Vector3(opponent.transform.position.x, transform.position.y, opponent.transform.position.z);
        transform.LookAt(lookAtPos);
    }

    public void TriggerRandomAnim(string[] anims)
    {
        int randomAnim = Random.Range(0, anims.Length);
        Animator.SetTrigger(anims[randomAnim]);
    }

}
