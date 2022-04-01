using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 공격력
    [SerializeField]
    private int damage = 1;

    // 처치시 점수 획득
   [SerializeField]
    private int scorePoint = 100;

    // 폭발
    [SerializeField]
    private GameObject explosionPrefab;

    private PlayerController playerController;

    // 적 체력
    [SerializeField]
    private float enemyMaxHP = 4;
    private float enemyCurrentHP;
    private SpriteRenderer spriteRenderer;

    // 아이템 드랍 배열(파워, 회복)
    [SerializeField]
    private GameObject[] itemPrefabs;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // 현재 체력을 최대 체력과 같게.
        enemyCurrentHP = enemyMaxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EnemyDamageHit(float damage)
    {
        // 체력 -damage만큼 감소
        enemyCurrentHP -= damage;

        StopCoroutine("HitAnimation");
        StartCoroutine("HitAnimation");

        // 체력 0 이하 적 사망
        if (enemyCurrentHP <= 0)
        {
            EnemyDie();
        }

    }

    private IEnumerator HitAnimation()
    {
        // 플레이어 색상 변경
        spriteRenderer.color = Color.red;
        //0.1초 대기
        yield return new WaitForSeconds(0.1f);
        // 원래 색상으로
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 체력감소
            collision.GetComponent<PlayerController>().DamageHit(damage);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void EnemyDie()
    {
        GameManager.instance.AddScore(scorePoint);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // 일정 확률 드랍
        ItemSpawn();
        Destroy(gameObject);
    }

    private void ItemSpawn()
    {
        // 파워 (10%)
        int itemSpawn = Random.Range(0, 100);
        if (itemSpawn < 10)
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
        }
    }
}
