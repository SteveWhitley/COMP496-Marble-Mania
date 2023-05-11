using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetAbility : MonoBehaviour
{
    public ControlPoint control;
    Vector3 lastVelocity;
    public AudioSource boing;
    

    void Update() 
    {
        lastVelocity = control.ball.velocity;
    }

    public void ricochet(Collision collision) {
        boing.Play(0);
        var speed = lastVelocity.magnitude * 1.25f;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        control.ball.constraints = RigidbodyConstraints.FreezePositionY;
        control.ball.velocity = direction * Mathf.Max(speed, 4f);
    }
}
