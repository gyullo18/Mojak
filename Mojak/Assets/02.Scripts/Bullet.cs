using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //collision.GetComponent<Enemy>().EnemyDie();
            collision.GetComponent<Enemy>().EnemyDamageHit(damage);
            Destroy(gameObject);
        }

        // �±װ� Boss���
        else if (collision.CompareTag("Boss"))
        {
            // ���� ü�� ����
            collision.GetComponent<BossHP>().BossDamaged(damage);
            // �Ѿ� ����
            Destroy(gameObject);
        }
    }
}
