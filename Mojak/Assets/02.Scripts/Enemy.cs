using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ���ݷ�
    [SerializeField]
    private int damage = 1;

    // óġ�� ���� ȹ��
   [SerializeField]
    private int scorePoint = 100;

    // ����
    [SerializeField]
    private GameObject explosionPrefab;

    private PlayerController playerController;

    // �� ü��
    [SerializeField]
    private float enemyMaxHP = 4;
    private float enemyCurrentHP;
    private SpriteRenderer spriteRenderer;

    // ������ ��� �迭(�Ŀ�, ȸ��)
    [SerializeField]
    private GameObject[] itemPrefabs;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // ���� ü���� �ִ� ü�°� ����.
        enemyCurrentHP = enemyMaxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EnemyDamageHit(float damage)
    {
        // ü�� -damage��ŭ ����
        enemyCurrentHP -= damage;

        StopCoroutine("HitAnimation");
        StartCoroutine("HitAnimation");

        // ü�� 0 ���� �� ���
        if (enemyCurrentHP <= 0)
        {
            EnemyDie();
        }

    }

    private IEnumerator HitAnimation()
    {
        // �÷��̾� ���� ����
        spriteRenderer.color = Color.red;
        //0.1�� ���
        yield return new WaitForSeconds(0.1f);
        // ���� ��������
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾� ü�°���
            collision.GetComponent<PlayerController>().DamageHit(damage);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void EnemyDie()
    {
        GameManager.instance.AddScore(scorePoint);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // ���� Ȯ�� ���
        ItemSpawn();
        Destroy(gameObject);
    }

    private void ItemSpawn()
    {
        // �Ŀ� (10%)
        int itemSpawn = Random.Range(0, 100);
        if (itemSpawn < 10)
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
        }
    }
}
