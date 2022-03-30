using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameover = false;
    //public Text scoreText;
    public GameObject gameoverUI;

    //private float TextTime = 0.1f;
    //private Text text;

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
        }
    }

    //private IEnumerator TextEffect(float start, float end)
    //{
    //    float currentTime = 0.0f;
    //    float percent = 0.0f;

    //    while (percent < 1)
    //    {
    //        currentTime += Time.deltaTime;
    //        percent = currentTime / TextTime;

    //        Color color = text.color;
    //        color.r = Mathf.Lerp(start, end, percent);
    //        color.g = Mathf.Lerp(start, end, percent);
    //        color.b = Mathf.Lerp(start, end, percent);
    //        text.color = color;

    //        yield return null;
    //    }
    //}

        public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }

}
