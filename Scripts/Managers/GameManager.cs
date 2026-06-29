using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour //class to handle game progression and score
{
    public static GameManager instance; //static instance

    [Header("Waves")]
    public int currentWave; //tracks the current wave
    public float waveCooldown; //cooldown between waves
    public bool waveActive;

    [Header("Scores")]
    public float currentScore; //tracks the players score
    public int waterLevel;
    int nextWaveScore = 5;

    [Header("Water Leaks")]
    public List<WaterLeakLogic> waterLeakPrefabs; //list to hold all of the waterLeak classes

    [Header("Lights")]
    public List<GameObject> ceilingLights;

    public float masterSensitivity;


    private void Awake()
    {
        if (instance == null) //null check
        {
            instance = this; //make this class instance
        }

        else
        {
            Destroy(gameObject); //destroy extras
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveCooldown = 10;
        currentWave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (waveCooldown > 0)
        {
            waveCooldown -= Time.deltaTime;
        }

        else if (waveCooldown < 0)
        {
            if (!waveActive)
            {
                //StartWave(currentWave);
                StartCoroutine(Wave(currentWave));
            }
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UIManager.instance.Score.text = currentScore.ToString();

        if (currentScore >= nextWaveScore)
        {
            currentWave++;
            nextWaveScore += 8;
        }
    }

    public void StartWave(int numberOfLeaks) //starts the wave that is next
    {
        waveActive = true;
        for (int i = 0;  i < numberOfLeaks; i++) //loop the number of times that is wanted
        {
            WaterLeakLogic chosenPosition = waterLeakPrefabs[Random.Range(0, waterLeakPrefabs.Count)]; //take a random waterleaklogic class from list of classes
            chosenPosition.ActivateWaterDrop(); //activate the chosen object
        }
    }

    public IEnumerator Wave(int numberOfLeaks)
    {
        waveActive = true;
        List<WaterLeakLogic> waterLeaks = new List<WaterLeakLogic>();

        for (int i = 0; i < numberOfLeaks; i++) //loop the number of times that is wanted
        {
            foreach (var LeakPrefab in waterLeakPrefabs)
            {
                if (!LeakPrefab.isActive)
                {
                    waterLeaks.Add(LeakPrefab);
                }
            }

            WaterLeakLogic chosenPosition = waterLeaks[Random.Range(0, waterLeaks.Count)]; //take a random waterleaklogic class from list of classes
            chosenPosition.ActivateWaterDrop(); //activate the chosen object
            yield return new WaitForSeconds(7);
        }

        waveCooldown = 20;
        waveActive = false;
        yield return null;
    }

    public void GameOVer()
    {
        SceneManager.LoadScene("DeathScene");
    }

    public void AddWaterLevel()
    {
        waterLevel++;

        if (waterLevel >= 10)
        {
            GameOVer();
        }

        else if (waterLevel == 5)
        {
            TurnOffLights();
        }

        else if (waterLevel == 8)
        {
            TurnOffLights();
        }
    }

    public void TurnOffLights()
    {
        Debug.Log("turning off lights");
        foreach (GameObject light in ceilingLights)
        {
            if (light.activeSelf)
            {
                Debug.Log("getting random number");
                int random = Random.Range(0, 10);

                if (random <= 3)
                {
                    if (light.transform.childCount > 0)
                    {
                        Transform firstChild = light.transform.GetChild(0);
                        firstChild.gameObject.SetActive(false);
                        Debug.Log(firstChild.gameObject.name + " switched off");
                    }
                }
            }
        }
    }
}
