using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { PowerUp = 0, Boom, HP }
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);

        movement.Move(new Vector3(x, y, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 아이템 효과
            Use(collision.gameObject);
            // 아이템 삭제
            Destroy(gameObject);
        }
    }

    public void Use(GameObject player)
    {
        switch (itemType)
        {
            case ItemType.PowerUp:
                player.GetComponent<Weapon>().Level ++;
                break;
            case ItemType.Boom:
                player.GetComponent<Weapon>().BoomCount++;
                break;
            case ItemType.HP:
                player.GetComponent<PlayerController>().CurrentHP += 2;
                break;
        }
    }
}
