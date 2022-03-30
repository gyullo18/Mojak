using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageSize stageSize;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnTime;

    private void Awake()
    {
        StartCoroutine("EnemySpawn");
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            float posX = Random.Range(stageSize.LimitMin.x, stageSize.LimitMax.x);
            Instantiate(enemyPrefab, new Vector3(posX, stageSize.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
