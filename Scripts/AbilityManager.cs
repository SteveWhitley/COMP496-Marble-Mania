using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public SpeedAbility speed;
    public RicochetAbility ricochet;
    public LightningAbility lightning;
    public PlayerTurn playerTurn;
    public ControlPoint control;
    
    bool p1HasAbility = true;
    bool p2HasAbility = true;

    bool abilityActivated = false;

    public Material speedMaterial;
    public Material lightningMaterial;

    // 0 = normal, 1 = speed, 2 = ricochet, 3 = lightning
    int p1AbilityType = 0;
    int p2AbilityType = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)) 
        {
            if (playerTurn.Player1Turn)
            {
                if (p1HasAbility && !abilityActivated)
                {
                    abilityActivated = true;
                    control.marble.GetComponent<MeshRenderer>().material = lightningMaterial;
                    // speed.speedChange();
                } else {
                    
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (abilityActivated) 
        {
            lightning.lightningStrike();
        }
    }
}
