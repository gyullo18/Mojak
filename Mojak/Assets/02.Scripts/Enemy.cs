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

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameManager.instance.AddScore(scorePoint);
        GameManager.instance.BestScore();
        Destroy(gameObject);
    }
}
