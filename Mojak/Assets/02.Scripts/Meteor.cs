using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // ���ݷ�
    [SerializeField]
    private int damage = 3;

    // ����
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //� ���ݷ¸�ŭ �÷��̾� ü�� ����
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
