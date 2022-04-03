using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField]
    private StageSize stageSize;
    [SerializeField]
    private GameObject alertLinePrefab;
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private float minSpawnTime = 1.0f;
    [SerializeField]
    private float maxSpawnTime = 4.0f;
    private bool isBossDie = false;

    private void Awake()
    {
        StartCoroutine("MeteorSpawn");
    }

    private IEnumerator MeteorSpawn()
    {
        if (isBossDie)
        {
            while (true)
            {
                float posX = Random.Range(stageSize.LimitMin.x, stageSize.LimitMax.x);
                GameObject alertLine = Instantiate(alertLinePrefab, new Vector3(posX, 0, 0), Quaternion.identity);

                yield return new WaitForSeconds(1.0f);

                Destroy(alertLine);

                Vector3 meteorPos = new Vector3(posX, stageSize.LimitMax.y + 1.0f, 0);
                Instantiate(meteorPrefab, meteorPos, Quaternion.identity);

                float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(spawnTime);
            }
        }
        else if (!isBossDie)
        {
            Destroy(gameObject);
        }  
    }
}
