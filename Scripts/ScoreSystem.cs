using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    // necessary game objects and UI elements
    public PlayerTurn playerTurn;
    public Text p1ScoreBoard;
    public Text p2ScoreBoard;
    public AudioSource scored;
    public AudioSource powerUp;
    public MarblesLeft marblesLeft;

    public bool abilityKnockedOut;
    public AbilityManager abilityManager;
    public AbilityMarbleSpawner marbleSpawner;
    // Tracks each player's score
    public int p1Score;
    public int p2Score;
    

    //PS: change layer if marbles bouncing into ring is a problem!
IEnumerator destroyMarble(GameObject other) 
    {
        yield return new WaitForSeconds(2);
        Destroy(other);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("greenMarble") || other.gameObject.CompareTag("blueMarble")) 
            {
                marblesLeft.marbleAdd();
            }
    }

    // detects whenever a scored marble leaves the arena
    private void OnTriggerExit(Collider other) 
    {
        // normal score green marbles
        if (other.gameObject.CompareTag("greenMarble")) 
        {
            if (playerTurn.Player1Turn)
            {
                p1Score += 1;
                p1ScoreBoard.text = "P1 Score: " + p1Score;
            } else 
            {
                p2Score += 1;
                p2ScoreBoard.text = p2Score + " :P2 Score";
            }
            StartCoroutine(destroyMarble(other.gameObject));
            scored.Play(0); 
        // double score blue marbles (harder to knockout)
        } else if (other.gameObject.CompareTag("blueMarble")) 
        {
            if (playerTurn.Player1Turn)
            {
                p1Score += 2;
                p1ScoreBoard.text = "P1 Score: " + p1Score;
            } else 
            {
                p2Score += 2;
                p2ScoreBoard.text = p2Score + " :P2 Score";
            }
            StartCoroutine(destroyMarble(other.gameObject));
            scored.Play(0);
        } else if (other.gameObject.CompareTag("lightningMarble") || other.gameObject.CompareTag("speedMarble") || other.gameObject.CompareTag("ricochetMarble")) 
        {
            if (playerTurn.Player1Turn)
            {
                abilityManager.p1HasAbility = true;
                if(other.gameObject.CompareTag("speedMarble"))
                {
                    abilityManager.p1AbilityType = 1; 
                    abilityManager.p1Ability.text = "Ability: \n SPEED";
                } else if(other.gameObject.CompareTag("ricochetMarble"))
                {
                    abilityManager.p1AbilityType = 2; 
                    abilityManager.p1Ability.text = "Ability: \n RICOCHET";
                } else if(other.gameObject.CompareTag("lightningMarble"))
                {
                    abilityManager.p1AbilityType = 3; 
                    abilityManager.p1Ability.text = "Ability: \n LIGHTNING";
                }
            } else 
            {
                abilityManager.p2HasAbility = true;
                if(other.gameObject.CompareTag("speedMarble"))
                {
                    abilityManager.p2AbilityType = 1; 
                    abilityManager.p2Ability.text = "Ability: \n SPEED";
                } else if(other.gameObject.CompareTag("ricochetMarble"))
                {
                    abilityManager.p2AbilityType = 2; 
                    abilityManager.p2Ability.text = "Ability: \n RICOCHET";
                } else if(other.gameObject.CompareTag("lightningMarble"))
                {
                    abilityManager.p2AbilityType = 3; 
                    abilityManager.p2Ability.text = "Ability: \n LIGHTNING";
                }
            }
            abilityKnockedOut = true;
            marbleSpawner.marbleInField = false;
            StartCoroutine(destroyMarble(other.gameObject));
            powerUp.Play(0);
        }
    }
}

