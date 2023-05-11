using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMarbleSpawner : MonoBehaviour
{
    // prevents having more than one ability in the game at a time
    public bool marbleInField = false;
    public bool attemptTaken = true;

    // abilities that can spawn in
    public GameObject speed;
    public GameObject ricochet;
    public GameObject lightning;

    public ScoreSystem score;

    public AbilityManager abilityManager;
    public Text abilityAnnounce;
    public GameObject spawner;
    Transform[] spawns;

    GameObject toSpawn;
    

    // adds each spawn area to a list then puts it in an array
    void Start() {
        spawns = spawner.GetComponentsInChildren<Transform>();
        abilityAnnounce.gameObject.SetActive(false);
    }

    // Checks if there's a marble in the playing field, and attempts to spawn one if not
    void Update()
    {
        if (!marbleInField && !attemptTaken) 
        {
            score.abilityKnockedOut = false;
            int whichSpawn = Random.Range(0,4);
            int spawnChance = Random.Range(1,11);
            if (spawnChance == 1)
            {
                toSpawn = speed;
                Instantiate(toSpawn, spawns[whichSpawn].position, Quaternion.identity);
                marbleInField = true;
                StartCoroutine(displayAnnounce());
            } else if (spawnChance == 2)
            {
                toSpawn = ricochet;
                Instantiate(toSpawn, spawns[whichSpawn].position, Quaternion.identity);
                marbleInField = true;
                StartCoroutine(displayAnnounce());
            } else if (spawnChance == 3)
            {
                toSpawn = lightning;
                Instantiate(toSpawn, spawns[whichSpawn].position, Quaternion.identity);
                marbleInField = true;
                StartCoroutine(displayAnnounce());
            }
            //prevents spawning before turn starts
            attemptTaken = true;
        }
    }

    IEnumerator displayAnnounce() 
    {
        abilityAnnounce.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        abilityAnnounce.gameObject.SetActive(false);
    }
}
