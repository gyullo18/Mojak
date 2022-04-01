using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TxtColor : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.1f;
    private Text bossWarningText;
    // Start is called before the first frame update
    void Awake()
    {
        bossWarningText = GetComponent<Text>(); 
    }

    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }


    private IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    private IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            bossWarningText.color = Color.Lerp(startColor, endColor, percent);

            yield return null;
        }
    }
}
