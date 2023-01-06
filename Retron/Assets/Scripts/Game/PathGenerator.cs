using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 100f;
    private const float PLATFORM_DESTROY_DISTANCE = 100f;

    private Vector3 rotationAngle;

    private List<GameObject> levelParts = new List<GameObject>();

    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject firstLevel;
    [SerializeField]
    private List<GameObject> generateLevel;


    private Vector3 spawnPos;

    private int rright = 0;
    private int lleft = 0;

    private void Awake()
    {
        rotationAngle.x = 0;
        rotationAngle.y = 0;
        rotationAngle.z = 0;

        firstLevel = Instantiate(firstLevel, Vector3.zero, transform.rotation);
        levelParts.Add(firstLevel);

        spawnPos = firstLevel.transform.Find("endPos").position;
        spawnPos.z += 39.8f;
        for (int i = 0; i < 3; i++)
        {
            SpawnLevelPart();
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.rb.position, spawnPos) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }

        if (levelParts.Count > 1)
        {
            if (Vector3.Distance(player.rb.position, levelParts[0].transform.position) > PLATFORM_DESTROY_DISTANCE)
            {
                DeleteParts();
            }
        }
    }

    private void SpawnLevelPart()
    {
        GameObject chosenLevelPart = generateLevel[UnityEngine.Random.Range(0, generateLevel.Count)];
        GameObject levelPart = SpawnLevelPart(chosenLevelPart, spawnPos);

        levelPart.transform.Rotate(rotationAngle, Space.World);

        spawnPos = levelPart.transform.Find("endPos").position;

        
        if (levelPart.tag == "toRight")
        {
            if (lleft != 0)
            {
                lleft--;
            }
            else
            {
                rright++;
            }

            rotationAngle.y += 90;
        }
        else if (levelPart.tag == "toLeft")
        {
            if (rright != 0)
            {
                rright--;
            }
            else
            {
                lleft++;
            }

            rotationAngle.y -= 90;
        }

        switch (rright)
        {
            case 1:
                spawnPos.x += 39.8f;
                break;

            case 2:
                spawnPos.z -= 39.8f;
                break;

            case 3:
                spawnPos.x -= 39.8f;
                break;

            case 4:
                rright = 0;
                rotationAngle.y = 0;
                break;
        }

        switch (lleft)
        {

            case 1:
                spawnPos.x -= 39.8f;
                break;

            case 2:
                spawnPos.z -= 39.8f;
                break;

            case 3:
                spawnPos.x += 39.8f;
                break;

            case 4:
                lleft = 0;
                rotationAngle.y = 0;
                break;
        }    

        if (rright == 0 && lleft == 0)
        {
            spawnPos.z += 39.8f;
        }
    }
    private GameObject SpawnLevelPart(GameObject level, Vector3 spawnPosition)
    {
        GameObject levelPart = Instantiate(level, spawnPosition, transform.rotation);
        levelParts.Add(levelPart);
        return levelPart;
    }

    private void DeleteParts()
    {
        Destroy(levelParts[0]);
        levelParts.RemoveAt(0);
    }
}
