using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour
{
    public bool Player1Turn;
    public Text PlayerGo;
    public GameObject PlayerMarble;
    public Transform Player1Spawn;
    public Transform Player2Spawn;
    public AbilityMarbleSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        Player1Turn = true;
        PlayerGo.gameObject.SetActive(true);
        PlayerGo.text = "Player 1 Go!";
        PlayerGo.color = Color.blue;
        PlayerMarble.transform.position =  new Vector3(Player1Spawn.position.x,Player1Spawn.position.y, Player1Spawn.position.z);
        StartCoroutine(displayText());
    }

    public void TurnSwitch() 
    {
        if (Player1Turn) {
            Player1Turn = false;
            PlayerGo.gameObject.SetActive(true);
            PlayerGo.text = "Player 2 Go!";
            PlayerGo.color = Color.red;
            PlayerMarble.transform.position = new Vector3(Player2Spawn.position.x,Player2Spawn.position.y, Player2Spawn.position.z);
            StartCoroutine(displayText());
        } else {
            Player1Turn = true;
            PlayerGo.gameObject.SetActive(true);
            PlayerGo.text = "Player 1 Go!";
            PlayerGo.color = Color.blue;
            PlayerMarble.transform.position =  new Vector3(Player1Spawn.position.x,Player1Spawn.position.y, Player1Spawn.position.z);
            StartCoroutine(displayText());
        }
        spawner.attemptTaken = false;
    }

    IEnumerator displayText() 
    {
        yield return new WaitForSeconds(2);
        PlayerGo.gameObject.SetActive(false);

    }

}
