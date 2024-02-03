using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Text restartText;

    private GameManager gameManager;

    public Image[] livesImage;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score 0";
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentLives <= 0)
        {
            DisplayGameOver();
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void DisplayGameOver()

    { 
      gameOverText.gameObject.SetActive(true);
        if(gameManager != null)
        {
            gameManager.GameOver();
            restartText.gameObject.SetActive(true);
        }
      
      StartCoroutine(GameOverEffect());
        
    }

    IEnumerator GameOverEffect()
    {
        while(true)
        {
            gameOverText.text = "Game Over!";
            yield return new WaitForSeconds(1f);

            gameOverText.text = " ";
            yield return new WaitForSeconds(1f);
        }

        
    }
}
