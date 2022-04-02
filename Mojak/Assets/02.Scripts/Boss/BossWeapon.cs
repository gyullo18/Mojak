using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AtkType { Circle = 0, FireToPlayer}

public class BossWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bossBulletPrefab;

    public void StartFire(AtkType atkType)
    {
        StartCoroutine(atkType.ToString());
    }

    public void StopFire(AtkType atkType)
    {
        StopCoroutine(atkType.ToString());
    }

    private IEnumerator Circle()
    {
        float atkRate = 0.5f;
        int count = 30;
        float iAngle = 360 / count;
        float wAngle = 0;

        // 원 형태 방사
        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
                // 발사체 이동 방향(각도)
                float angle = wAngle + iAngle * i;
                // 발사체 이동 방향(벡터)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                // 발사체 이동방향 설정
                clone.GetComponent<Movement>().Move(new Vector2(x, y));
            }
            // 발사체가 생성되는 시작 각도 설정 변수
            wAngle += 1;

            // 공격 주기만큼 대기
            yield return new WaitForSeconds(atkRate);
        }
    }

    private IEnumerator FireToPlayer()
    {
        // 목표 = 중앙
        Vector3 targetPos = Vector3.zero;
        float atkRate = 0.1f;

        while (true)
        {
            // 발사체 생성
            GameObject clone = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
            // 발사체 이동방향
            Vector3 direction = (targetPos - clone.transform.position).normalized;
            // 발사체 이동 방향 설정
            clone.GetComponent<Movement>().Move(direction);

            // atkRate시간 만큼 대기
            yield return new WaitForSeconds(atkRate);
        }
    }

}
