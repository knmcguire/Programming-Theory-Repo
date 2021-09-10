using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public Text amountOfDronesText;
    public Text amountOfCollectedDronesText;
    public Text averageFlightTimeText;

    public Text PlayerNameText;

    public Text BestScoreText;

    public Text TimeOverText;

    public Text YourScoreText;

    public GameObject GameOverText;

    int totalFoundDrones;

    private bool m_GameOver = false;

    int timeBeforeGameOver = 120;


    // Start is called before the first frame update
    void Start()
    {

        NameSaver.Instance.LoadPlayerScore();
        amountOfDronesText.text = $"Total Amount of Drones : 0";
        amountOfCollectedDronesText.text = $"Amount of Collected Drones : 0";
        averageFlightTimeText.text=  $"Average Flight Time Swarm : 0";
        PlayerNameText.text = "Player:  " + NameSaver.Instance.PlayerName;
        BestScoreText.text = $"Best Score : {NameSaver.Instance.BestPlayerName} : {NameSaver.Instance.HighScore}";

    }


    // Update is called once per frame
    void Update()
    {
         UpdateAverageFlightTime();

         int timeLeft = timeBeforeGameOver - (int)Time.timeSinceLevelLoad;
        
         if(Time.timeSinceLevelLoad > timeBeforeGameOver && m_GameOver == false)
         {
            GameOver();
         } 

         if(!m_GameOver)  
         {
            TimeOverText.text = $"Time Left: {timeLeft} seconds";

         }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        NameSaver.Instance.CurrentScore = totalFoundDrones;
        YourScoreText.text = $"Your Score : {totalFoundDrones}";

        NameSaver.Instance.SavePlayerScore();
        BestScoreText.text = $"Best Score : {NameSaver.Instance.BestPlayerName} : {NameSaver.Instance.HighScore}";
    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }    

    public void UpdateAverageFlightTime()
    {
        float totalFlightTime = 0;
        int totalCollectedDrones = 0;
        var foundCrazyflieObjects =FindObjectsOfType<Crazyflie>();

        foreach(Crazyflie crazyflie in foundCrazyflieObjects)
        {
            if(crazyflie.isCollected)
            {
                totalFlightTime += crazyflie.currentFlightTime;
                totalCollectedDrones ++;
            }
        }

        var foundBoltObjects =FindObjectsOfType<Bolt>();

        foreach(Bolt bolt in foundBoltObjects)
        {
            if(bolt.isCollected)
            {
                totalFlightTime += bolt.currentFlightTime;
                totalCollectedDrones ++;
            }
        }

        if( foundCrazyflieObjects.Length != 0)
        {
            float averageFlightTime = 0.0f;
            if(totalCollectedDrones != 0)
                averageFlightTime = totalFlightTime / (float)totalCollectedDrones;

            averageFlightTimeText.text = $"Average Flight Time Swarm : {averageFlightTime}";
        }

    }


    public void UpdateAmountOfDrones(int amountOfDrones)
    {
        amountOfDronesText.text = $"Total Amount of Drones: {amountOfDrones}";
    }

    public void UpdateAmountOfCollectedDrones(int amountOfDrones)
    {
        amountOfCollectedDronesText.text = $"Amount of Collected Drones : {amountOfDrones}";
        totalFoundDrones = amountOfDrones;
    }
}
