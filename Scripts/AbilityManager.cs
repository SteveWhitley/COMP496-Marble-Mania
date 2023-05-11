using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public SpeedAbility speed;
    public RicochetAbility ricochet;
    public LightningAbility lightning;

    public PlayerTurn playerTurn;
    public ControlPoint control;
    public ScoreSystem scoreSystem;

    public bool p1HasAbility = false;
    public bool p2HasAbility = false;

    public bool abilityActivated = false;
    bool abilityUsed;

    public Material speedMaterial;
    public Material lightningMaterial;
    public Material ricochetMaterial;
    public Material normalMaterial;

    public AudioSource powerOn;
    public AudioSource powerOff;

    // 0 = normal, 1 = speed, 2 = ricochet, 3 = lightning
    public int p1AbilityType = 0;
    public int p2AbilityType = 0;

    public Text p1Ability;
    public Text p2Ability;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && !control.turnTaken) 
        {
            if (playerTurn.Player1Turn)
            {
                if (p1HasAbility && !abilityActivated)
                {
                    abilityActivated = true;
                    //Speed marble ability activates
                    if (p1AbilityType == 1)
                    {
                        speed.speedChange();
                        control.marble.GetComponent<MeshRenderer>().material = speedMaterial;
                    //Ricochet marble ability activates
                    } else if (p1AbilityType == 2)
                    {
                        control.marble.GetComponent<MeshRenderer>().material = ricochetMaterial;
                    //Lightning marble ability activates
                    } else if (p1AbilityType == 3)
                    {
                        control.marble.GetComponent<MeshRenderer>().material = lightningMaterial;
                    }
                    powerOn.Play(0);
                } else {
                    abilityActivated = false;
                    speed.speedRevert();
                    control.marble.GetComponent<MeshRenderer>().material = normalMaterial;
                    powerOff.Play(0);
                }
            } else {
                if (p2HasAbility && !abilityActivated)
                {
                    abilityActivated = true;
                    //Speed marble ability activates
                    if (p2AbilityType == 1)
                    {
                        speed.speedChange();
                        control.marble.GetComponent<MeshRenderer>().material = speedMaterial;
                    //Ricochet marble ability activates
                    } else if (p2AbilityType == 2)
                    {
                        control.marble.GetComponent<MeshRenderer>().material = ricochetMaterial;
                    //Lightning marble ability activates
                    } else if (p2AbilityType == 3)
                    {
                        control.marble.GetComponent<MeshRenderer>().material = lightningMaterial;
                    }
                    powerOn.Play(0);
                } else {
                    abilityActivated = false;
                    speed.speedRevert();
                    control.marble.GetComponent<MeshRenderer>().material = normalMaterial;
                    powerOff.Play(0);
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0) && abilityActivated) {
            abilityUsed = true;
            StartCoroutine(abilityTimeout());
        }
    }

     

    private void OnCollisionEnter(Collision other) {
        if (abilityActivated && playerTurn.Player1Turn) 
        {
            //ricochet ability auto deactivates when turn ends
            if (p1AbilityType == 2)
            {
                ricochet.ricochet(other);
            } else if (p1AbilityType == 3)
            {
                lightning.lightningStrike();
                abilityActivated = false;
            }
        } else if (abilityActivated && !playerTurn.Player1Turn) 
        {
            //ricochet ability auto deactivates when turn ends
            if (p2AbilityType == 2)
            {
                ricochet.ricochet(other);
            } else if (p2AbilityType == 3)
            {
                lightning.lightningStrike();
                abilityActivated = false;
            }
        }
    }

    public void AbilityUsed(bool newAbility) 
    {
        if (abilityUsed && playerTurn.Player1Turn && !newAbility)
        {
            p1AbilityType = 0;
            p1Ability.text = "Ability: \n none";
            abilityActivated = false;
            p1HasAbility = false;
        } else if (abilityUsed && !playerTurn.Player1Turn && !newAbility)
        {
            p2AbilityType = 0;
            p2Ability.text = "Ability: \n none";
            abilityActivated = false;
            p2HasAbility = false;
        }
        abilityActivated = false;
        abilityUsed = false;
    }

    IEnumerator abilityTimeout() 
     {
        yield return new WaitForSeconds(4);
        speed.speedRevert();
        AbilityUsed(scoreSystem.abilityKnockedOut);
     }
}
