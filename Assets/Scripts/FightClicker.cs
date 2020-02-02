using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightClicker : MonoBehaviour
{
    public FightingAnimations myAnimations;
    public FightSounds mySounds;

    public SpriteRenderer myHealth;
    public SpriteRenderer enemyHealth;

    void OnMouseDown()
    {
        Debug.Log("click");
        myAnimations.TriggerRandomAnim(myAnimations.fightTriggers);
        mySounds.PlayRandomSoundRandomPitch(mySounds.fightHits, 1f);

        myHealth.transform.localScale = new Vector3(myHealth.transform.localScale.x * 2, myHealth.transform.localScale.y, myHealth.transform.localScale.z);
        enemyHealth.transform.localScale = new Vector3(enemyHealth.transform.localScale.x / 2, enemyHealth.transform.localScale.y, enemyHealth.transform.localScale.z);
    }
}
