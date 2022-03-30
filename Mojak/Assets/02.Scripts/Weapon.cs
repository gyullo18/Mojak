using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float firingSpeed = 0.2f; // 발사속도

    public void StartFiring()
    {
        StartCoroutine("Attack");
    }

    public void StopFiring()
    {
        StopCoroutine("Attack");
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(firingSpeed);
        }
    }
}
