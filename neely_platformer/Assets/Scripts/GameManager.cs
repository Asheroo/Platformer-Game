using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Health;
    public GameObject Timer;
    public GameObject player;
    public int nextLevel;
    
    private float currentTime;
    private bool currentlyPaused;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        nextLevel = 0;
        currentlyPaused = false;

    }

    // Update is called once per frame
    void Update()
    {

        currentTime = Mathf.RoundToInt(Time.time);

        Health.GetComponent<Text>().text = "Health: " + player.GetComponent<playerController>().health;

        Timer.GetComponent<Text>().text = "Timer: " + currentTime;

        if (nextLevel == 1)
        {
            SceneManager.LoadScene("Level 2");
                
        }

        else if (nextLevel == 2)
        {
            SceneManager.LoadScene("Main Menu");

        }

        if (Input.GetKeyDown(KeyCode.Escape) && !currentlyPaused)
        {
            Time.timeScale = 0;
            currentlyPaused = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && currentlyPaused)
        {
            Time.timeScale = 1;
            currentlyPaused = false;
        }
    }
}
