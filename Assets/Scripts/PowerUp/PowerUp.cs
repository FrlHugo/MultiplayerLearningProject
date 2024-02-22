using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : NetworkBehaviour
{
    public PowerUpEffectSO powerUpEffect;
    public bool canBeDestroyed = false;
    [SerializeField] float invincibleTimeAfterSpawn = 0.4f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            Destroy(gameObject);
            PlayPowerUpSound();
            powerUpEffect.Apply(other.gameObject);
        }
        
    }
    private void Start()
    {
        canBeDestroyed = false;
        StartCoroutine(SpawnInvincibleTiming());
    }




    IEnumerator SpawnInvincibleTiming()
    {
        yield return new WaitForSeconds(invincibleTimeAfterSpawn);
        canBeDestroyed = true;

    }
    private void PlayPowerUpSound()
    {

    }

    public void DestroyPowerUp()
    {
        if(canBeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
