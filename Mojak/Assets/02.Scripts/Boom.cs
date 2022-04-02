using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    //[SerializeField]
    //private AudioClip boomAudio;
    private float boomDelay = 0.5f;
    private Animator animator;
    //private AudioSource audioSource;

    // ��ź ������
    [SerializeField]
    private int damage = 100;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //AudioSource = GetComponent<AudioSource>();

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = Vector3.zero;
        float current = 0;
        float percent = 0;

        while (percent <1)
        {
            current += Time.deltaTime;
            percent = current / boomDelay;
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(percent));

            yield return null;
        }

        animator.SetTrigger("OnBoom");
        //audioSource.clip = boomAudio;
        //audioSource.play();
    }

    public void OnBoom()
    {
        // ��ź �߻� �� ��� ��, ���׿�, �����Ѿ� ����
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Meteor");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("BossBullet");

        // ��� ��
        for (int i = 0; i < enemys.Length; ++i)
        {
            enemys[i].GetComponent<Enemy>().EnemyDie();
        }
        // ��� ���׿�
        for (int i =0; i < meteors.Length; ++i)
        {
            meteors[i].GetComponent<Meteor>().OnDie();
        }
        // ������ ��� �Ѿ�
        for (int i = 0; i< bullets.Length; ++i)
        {
            bullets[i].GetComponent<BossBullet>().OnDie();
        }
        // Boss�±׸� ���� ������Ʈ ���� ������
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null)
        {
            // ���� ü�� ����
            boss.GetComponent<BossHP>().BossDamaged(damage);
        }
        Destroy(gameObject);
    }
}
