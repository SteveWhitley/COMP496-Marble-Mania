using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAbility : MonoBehaviour
{

    public ControlPoint control;
    

    public float radius = 2.0F;
    public float power = 200.0F;
    public AudioSource strike;

    public void lightningStrike()
    {
        strike.Play(0);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders) 
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius);
            }
        }
    }
}
