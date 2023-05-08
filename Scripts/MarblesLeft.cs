using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarblesLeft : MonoBehaviour
{
    public int marbleCounter = 0;
    public ScoreSystem score;
    public GameObject BMarbles;
    public GameObject GMarbles;
    public Text marblesLeft;

    IEnumerator gameEnd() 
    {
        int player1Score = score.p1Score;
        int player2Score = score.p2Score;
        yield return new WaitForSeconds(5);
        if (player1Score > player2Score) {
            marblesLeft.text = "Player 1 Wins!";
        } else if (player1Score < player2Score) {
            marblesLeft.text = "Player 2 Wins!";
        } else {
            marblesLeft.text = "Tie!";
        }
        yield return new WaitForSeconds(5);
        Application.LoadLevel(Application.loadedLevel);
    }

    //takes count of the marbles in the arena
    public void marbleAdd() 
    {
        marbleCounter+= 1;
        marblesLeft.text = "Marbles Left: " + marbleCounter;
        
    }

    public void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("greenMarble") || other.gameObject.CompareTag("blueMarble")) 
        {
            marbleCounter--;
            marblesLeft.text = "Marbles Left: " + marbleCounter;
        }

        // will end game 
        if (marbleCounter == 0) 
        {
            StartCoroutine(gameEnd());
        }
    }
}
