using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAbility : MonoBehaviour
{

    public ControlPoint control;
    

    public float radius = 5.0F;
    public float power = 10.0F;

    public void lightningStrike()
    {
        
        Vector3 explosionPos = control.ball.position;
        control.ball.AddExplosionForce(power, explosionPos, radius, 3.0F);
    }
}
