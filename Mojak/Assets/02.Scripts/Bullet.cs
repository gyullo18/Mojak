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

        // 태그가 Boss라면
        else if (collision.CompareTag("Boss"))
        {
            // 보스 체력 감소
            collision.GetComponent<BossHP>().BossDamaged(damage);
            // 총알 삭제
            Destroy(gameObject);
        }
    }
}
