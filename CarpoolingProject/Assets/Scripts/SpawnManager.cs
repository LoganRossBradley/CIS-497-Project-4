using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] characters = new GameObject[5];
    public Vector3[] spawnLocations = new Vector3[10];

    public int randChar;
    public int randLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnCharacter(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnCharacter(int spawnAmount)
    {
        int[] locationsUsed = new int[spawnAmount];

        //create characters to pick up in random pos equal to ammount passed in
        for(int i = 0; i < spawnAmount; i +=0)
        {
            randLocation = UnityEngine.Random.Range(0, spawnLocations.Length - 1);
            //checks to see if random location has already been used
            if(!locationsUsed.Contains(randLocation))
            {
                randChar = UnityEngine.Random.Range(0, characters.Length - 1);
                Instantiate(characters[randChar], spawnLocations[randLocation], characters[randChar].transform.rotation);
                locationsUsed[i] = randLocation;
                i++;
            }
        }
    }
}
