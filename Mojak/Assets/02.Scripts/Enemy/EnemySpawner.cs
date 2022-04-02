using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 적생성 - 스테이지 크기
    [SerializeField]
    private StageSize stageSize;
    // 복제 생성할 적 프리펩
    [SerializeField]
    private GameObject enemyPrefab;
    // 생성 주기
    [SerializeField]
    private float spawnTime;
    // 적 체력 프리펩
    [SerializeField]
    private GameObject enemyHPPrefab;
    // 캔버스 오브젝트
    [SerializeField]
    private Transform canvas;
    // 스테이지 내 최대 적 수
    [SerializeField]
    private int maxEnemy = 20;

    // 배경음악 설정(보스 등장시 변경)
    [SerializeField]
    private AudioEffect audioEffect;
    // 보스 등장 텍스트 오브젝트
    [SerializeField]
    private GameObject BossWarningText;
    // 보스 오브젝트
    [SerializeField]
    private GameObject boss;
    // 보스 체력 패널
    [SerializeField]
    private GameObject panelBossHP;
    private void Awake()
    {
        // 보스 등장 텍스트 비활
        BossWarningText.SetActive(false);
        // 보스 체력 패널 비활
        panelBossHP.SetActive(false);
        // 보스 오브젝트 비활
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
        // 보스 등장 브금 설정
        audioEffect.ChangeBgm(BGMType.Boss);
        // 보스 등장 텍스트 활성
        BossWarningText.SetActive(true);
        // 1초 대기
        yield return new WaitForSeconds(1.0f);
        
        // 보스 등장 텍스트 비활
        BossWarningText.SetActive(false);
        // 보스 체력 패널 활성
        panelBossHP.SetActive(true);
        // 보스 활성
        boss.SetActive(true);
        // 보스 지정된 위치로 이동
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppear);
    }

}
