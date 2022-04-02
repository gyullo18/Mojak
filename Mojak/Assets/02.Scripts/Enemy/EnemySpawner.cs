using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // ������ - �������� ũ��
    [SerializeField]
    private StageSize stageSize;
    // ���� ������ �� ������
    [SerializeField]
    private GameObject enemyPrefab;
    // ���� �ֱ�
    [SerializeField]
    private float spawnTime;
    // �� ü�� ������
    [SerializeField]
    private GameObject enemyHPPrefab;
    // ĵ���� ������Ʈ
    [SerializeField]
    private Transform canvas;
    // �������� �� �ִ� �� ��
    [SerializeField]
    private int maxEnemy = 20;

    // ������� ����(���� ����� ����)
    [SerializeField]
    private AudioEffect audioEffect;
    // ���� ���� �ؽ�Ʈ ������Ʈ
    [SerializeField]
    private GameObject BossWarningText;
    // ���� ������Ʈ
    [SerializeField]
    private GameObject boss;
    // ���� ü�� �г�
    [SerializeField]
    private GameObject panelBossHP;
    private void Awake()
    {
        // ���� ���� �ؽ�Ʈ ��Ȱ
        BossWarningText.SetActive(false);
        // ���� ü�� �г� ��Ȱ
        panelBossHP.SetActive(false);
        // ���� ������Ʈ ��Ȱ
        boss.SetActive(false);
        StartCoroutine("EnemySpawn");
    }

    private IEnumerator EnemySpawn()
    {
        // �� ���� �� ����
        int currentEnemy = 0;
        while (true)
        {
            float posX = Random.Range(stageSize.LimitMin.x, stageSize.LimitMax.x);
            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(posX, stageSize.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            // �� ü�� ui
            EnemyHP(enemyClone);

            // �� ���� �� ����
            currentEnemy++;
            // �ִ� ������ �����ϸ� �� ���� �ڷ�ƾ ����, ���� ���� �ڷ�ƾ ����
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

    // ���� �ڸ�ƾ
    private IEnumerator BossSpawn()
    {
        // ���� ���� ��� ����
        audioEffect.ChangeBgm(BGMType.Boss);
        // ���� ���� �ؽ�Ʈ Ȱ��
        BossWarningText.SetActive(true);
        // 1�� ���
        yield return new WaitForSeconds(1.0f);
        
        // ���� ���� �ؽ�Ʈ ��Ȱ
        BossWarningText.SetActive(false);
        // ���� ü�� �г� Ȱ��
        panelBossHP.SetActive(true);
        // ���� Ȱ��
        boss.SetActive(true);
        // ���� ������ ��ġ�� �̵�
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppear);
    }

}
