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
    [SerializeField]
    private GameObject enemyHPPrefab;
    [SerializeField]
    private Transform canvas;
 

    private void Awake()
    {
        StartCoroutine("EnemySpawn");
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            float posX = Random.Range(stageSize.LimitMin.x, stageSize.LimitMax.x);
            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(posX, stageSize.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            // Àû Ã¼·Â ui
            EnemyHP(enemyClone);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void EnemyHP(GameObject enemy)
    {
        GameObject enemyHPClone = Instantiate(enemyHPPrefab);
        enemyHPClone.transform.SetParent(canvas);
        enemyHPClone.transform.localScale = Vector3.one;
    }
}
