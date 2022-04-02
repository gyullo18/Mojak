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

        // �� ���� ���
        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
                // �߻�ü �̵� ����(����)
                float angle = wAngle + iAngle * i;
                // �߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                // �߻�ü �̵����� ����
                clone.GetComponent<Movement>().Move(new Vector2(x, y));
            }
            // �߻�ü�� �����Ǵ� ���� ���� ���� ����
            wAngle += 1;

            // ���� �ֱ⸸ŭ ���
            yield return new WaitForSeconds(atkRate);
        }
    }

    private IEnumerator FireToPlayer()
    {
        // ��ǥ = �߾�
        Vector3 targetPos = Vector3.zero;
        float atkRate = 0.1f;

        while (true)
        {
            // �߻�ü ����
            GameObject clone = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
            // �߻�ü �̵�����
            Vector3 direction = (targetPos - clone.transform.position).normalized;
            // �߻�ü �̵� ���� ����
            clone.GetComponent<Movement>().Move(direction);

            // atkRate�ð� ��ŭ ���
            yield return new WaitForSeconds(atkRate);
        }
    }

}
