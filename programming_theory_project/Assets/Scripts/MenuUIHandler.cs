using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]


public class MenuUIHandler : MonoBehaviour
{


    public void EnterPlayerName(InputField NewName)
    {
        NameSaver.Instance.PlayerName = NewName.text;
    }

    public void StartNew()
    {
        SceneManager.LoadScene("FlightArena");
    }

    public Text BestScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        NameSaver.Instance.LoadPlayerScore();
        BestScoreText.text = $"Best Score : {NameSaver.Instance.BestPlayerName} : {NameSaver.Instance.HighScore}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
