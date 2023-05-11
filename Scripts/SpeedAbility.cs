using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : MonoBehaviour
{
    public ControlPoint control;

    public void speedChange() 
    {
        control.shootPower = 60f;
    }

    public void speedRevert() 
    {
        control.shootPower = 20f;
    }

}
