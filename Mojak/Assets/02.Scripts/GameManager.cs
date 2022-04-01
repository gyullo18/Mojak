using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameover = false;

    [SerializeField]
    private PlayerController playerController;

    public Text scoreText;
    public Text recordText;
    public GameObject gameoverUI; // 게임오버시 활성화할 UI 오브젝트.

    private int score = 0;

    


    //private void Start()
    //{
    //    Time.timeScale = 1;
    //}
    private void Awake()
    {
       

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    
    void Update()
    {

        if (isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }

  

    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
        GameManager.instance.BestScore();
        Time.timeScale = 0;
    }

    public void AddScore(int newScore)
    {
        if (isGameover) return;
        // 점수 출력
        score += newScore;
        scoreText.text = "Score :" + score;
        Debug.Log("점수");
    }

    public void BestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        if (bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        recordText.text = "BestScore :" + bestScore;
    }

    
}
