using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/BombPowerBuff")]
public class BombPowerBuff : PowerUpEffectSO
{
    public int amount;

    public override void Apply(GameObject target)
    {
        if((target.GetComponent<BomberMan>().bombPower + amount) > 13)
        {
            target.GetComponent<BomberMan>().bombPower = 13;
        }
        else
        {
            target.GetComponent<BomberMan>().bombPower += amount;
        }


    }


}
