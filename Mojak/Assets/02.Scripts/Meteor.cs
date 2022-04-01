using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // 공격력
    [SerializeField]
    private int damage = 3;

    // 폭발
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //운석 공격력만큼 플레이어 체력 감소
            collision.GetComponent<PlayerController>().DamageHit(damage);
            //Destroy(gameObject);
            OnDie();
        }
    }

    public void OnDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
