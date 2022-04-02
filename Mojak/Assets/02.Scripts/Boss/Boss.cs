using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BossState {MoveToAppear = 0, Phase1, Phase2, Phase3 }
public class Boss : MonoBehaviour
{
    [SerializeField]
    private StageSize stageSize;
    // ���� ��� ������
    [SerializeField]
    private GameObject bossExplosionPrefab;
    [SerializeField]
    private float bossAppear = 2.5f;
    private BossState bossState = BossState.MoveToAppear;
    private Movement movement;
    private BossWeapon bossWeapon;
    private BossHP bossHP;

    public GameObject GameClearText;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppear()
    {
        movement.Move(Vector3.down);

        while (true)
        {
            if ( transform.position.y <= bossAppear)
            {
                movement.Move(Vector3.zero);
                ChangeState(BossState.Phase1);
            }
            yield return null;
        }
    }

    private IEnumerator Phase1()
    {
        bossWeapon.StartFire(AtkType.Circle);

        while (true)
        {
            // ������ ü���� 70%���ϰ� �Ǹ�
            if(bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)
            {
                // �� �߻� ���� ����
                bossWeapon.StopFire(AtkType.Circle);
                ChangeState(BossState.Phase2);
            } 

            // ���� ü���� 30%���ϰ� �Ǹ�
            if (bossHP.CurrentHP <= bossHP.MaxHP * 03f)
            {
                bossWeapon.StopFire(AtkType.FireToPlayer);
                ChangeState(BossState.Phase3);
            }
            yield return null;
        }
    }

    private IEnumerator Phase2()
    {
        // �÷��̾� ��ġ�� ���� ����
        bossWeapon.StartFire(AtkType.FireToPlayer);

        // ó�� �̵� ������ ����������
        Vector3 direction = Vector3.right;
        movement.Move(direction);

        while (true)
        {
            // �¿� �̵� �� ���� ���� �����ϰ� �Ǹ� ������ �ݴ��
            if ( transform.position.x <= stageSize.LimitMin.x ||
                transform.position.x >= stageSize.LimitMax.x)
            {
                direction *= -1;
                movement.Move(direction);
            }
            yield return null;
        }
        
    }

    private IEnumerator Phase3()
    {
        // �� ���� ���� ����
        bossWeapon.StartFire(AtkType.Circle);
        // �÷��̾� ��ġ�� ���� ����
        bossWeapon.StartFire(AtkType.FireToPlayer);

        // ó�� �̵� ������ ����������
        Vector3 direction = Vector3.right;
        movement.Move(direction);

        while (true)
        {
            // �¿� �̵� �� ���� ���� �����ϰ� �Ǹ� ������ �ݴ��
            if (transform.position.x <= stageSize.LimitMin.x ||
                transform.position.x >= stageSize.LimitMax.x)
            {
                direction *= -1;
                movement.Move(direction);
            }
        }
    }

    public void BossDie()
    {
        // ���� �ı� ����Ʈ ����
        Instantiate(bossExplosionPrefab, transform.position, Quaternion.identity);
        // ���� ������Ʈ ����
        Destroy(gameObject);
        GameManager.instance.AddScore(10000);
        GameClearText.SetActive(true);
        GameManager.instance.BestScore();

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
