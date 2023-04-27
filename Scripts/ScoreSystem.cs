using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    // necessary game objects and UI elements
    public PlayerTurn playerTurn;
    public GameObject BMarbles;
    public GameObject GMarbles;
    public Text p1ScoreBoard;
    public Text p2ScoreBoard;
    public AudioSource scored;

    // Tracks each player's score
    public int p1Score;
    public int p2Score;
    

    //PS: change layer if marbles bouncing into ring is a problem!
IEnumerator destroyMarble(GameObject other) 
    {
        yield return new WaitForSeconds(2);
        Destroy(other);
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
        }
        
    }
}

