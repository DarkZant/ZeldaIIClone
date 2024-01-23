using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    const int PlayerLayer = 10;
    public int DamageDealt = 10;
    public PlayerCombat Player;
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == PlayerLayer)
            //Player.PlayerCombat.Hurt(DamageDealt);
    }
}
