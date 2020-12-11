using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] roadblocks = new GameObject[3];
    public GameObject[] characters = new GameObject[5];
    public Vector3[] characterLocations = new Vector3[10];

    public int randChar;
    public int randLocation;
    public int randRoadblock;

    // Start is called before the first frame update
    void Start()
    {
        //set scene this object is in to active scene so objects properly spawn in it instead of main menu scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameManager.CurrentLevelName));

        //max 12
        spawnCharacter(5);
        //max 10
        spawnParkedCars(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnParkedCars(int spawnAmount)
    {
        int[] carsUsed = new int[spawnAmount];

        //create characters to pick up in random pos equal to ammount passed in
        for (int i = 0; i < spawnAmount; i += 0)
        {
            randRoadblock = UnityEngine.Random.Range(0, roadblocks.Length - 1);
            //checks to see if random location has already been used
            if (!carsUsed.Contains(randRoadblock))
            {
                //randChar = UnityEngine.Random.Range(0, characters.Length - 1);
                //Instantiate(characters[randChar], CharacterLocations[randRoadblock], characters[randChar].transform.rotation);
                roadblocks[randRoadblock].SetActive(true);
                carsUsed[i] = randRoadblock;
                i++;
            }
        }
    }

    public void spawnCharacter(int spawnAmount)
    {
        int[] locationsUsed = new int[spawnAmount];

        //create characters to pick up in random pos equal to ammount passed in
        for(int i = 0; i < spawnAmount; i +=0)
        {
            randLocation = UnityEngine.Random.Range(0, characterLocations.Length - 1);
            //checks to see if random location has already been used
            if(!locationsUsed.Contains(randLocation))
            {
                randChar = UnityEngine.Random.Range(0, characters.Length - 1);
                Instantiate(characters[randChar], characterLocations[randLocation], characters[randChar].transform.rotation);
                locationsUsed[i] = randLocation;
                i++;
            }
        }
    }
}
