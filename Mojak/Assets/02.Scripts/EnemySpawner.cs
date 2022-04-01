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

    // 스테이지 내 최대 적 수
    [SerializeField]
    private int maxEnemy = 20;

    // 배경음악 설정(보스 등장시 변경)
    [SerializeField]
    private AudioEffect audioEffect;
    [SerializeField]
    private GameObject BossWarningText;
    // 보스
    [SerializeField]
    private GameObject boss;

    private void Awake()
    {
        BossWarningText.SetActive(false);
        boss.SetActive(false);
        StartCoroutine("EnemySpawn");
    }

    private IEnumerator EnemySpawn()
    {
        // 적 생성 수 변수
        int currentEnemy = 0;
        while (true)
        {
            float posX = Random.Range(stageSize.LimitMin.x, stageSize.LimitMax.x);
            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(posX, stageSize.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            // 적 체력 ui
            EnemyHP(enemyClone);

            // 적 생성 수 증가
            currentEnemy++;
            // 최대 수까지 생성하면 적 생성 코루틴 중지, 보스 생성 코루틴 시작
            if (currentEnemy == maxEnemy)
            {
                StartCoroutine("BossSpawn");
                break;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
    private void EnemyHP(GameObject enemy)
    {
        GameObject enemyHPClone = Instantiate(enemyHPPrefab);
        enemyHPClone.transform.SetParent(canvas);
        enemyHPClone.transform.localScale = Vector3.one;
    }

    // 보스 코르틴
    private IEnumerator BossSpawn()
    {
        audioEffect.ChangeBgm(BGMType.Boss);
        BossWarningText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        BossWarningText.SetActive(false);
        boss.SetActive(true);
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppear);
    }

}
