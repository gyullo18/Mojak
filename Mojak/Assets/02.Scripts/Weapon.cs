using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ÃÑ¾Ë
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float firingSpeed = 0.2f; // ¹ß»ç¼Óµµ

    // ¾ÆÀÌÅÛ
    // ÆÄ¿ö
    [SerializeField]
    private int maxLevel = 3;
    private int level = 1;
    // ÆøÅº
    [SerializeField]
    private GameObject boomPrefab;
    private int boomCount = 3;
    public int BoomCount
    {
        set => boomCount = Mathf.Max(0, value);
        get => boomCount;
    }

    public int Level
    {
        set => level = Mathf.Clamp(value, 1, maxLevel);
        get => level;
    }

    public void StartFiring()
    {
        StartCoroutine("Attack");
    }

    public void StopFiring()
    {
        StopCoroutine("Attack");
    }

    public void StartBoom()
    {
        if (boomCount > 0)
        {
            boomCount--;
            Instantiate(boomPrefab, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            AttackLevel();
            //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(firingSpeed);
        }
    }

    private void AttackLevel()
    {
        GameObject cloneBullet = null;

        switch (level)
        {
            case 1:
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(bulletPrefab, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Instantiate(bulletPrefab, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
            case 3:
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                cloneBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                cloneBullet.GetComponent<Movement>().Move(new Vector3(-0.2f, 1, 0));
                cloneBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                cloneBullet.GetComponent<Movement>().Move(new Vector3(0.2f, 1, 0));
                break;
        }
    }
}
