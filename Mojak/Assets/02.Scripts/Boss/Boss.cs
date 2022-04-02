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
    // 보스 사망 프리펩
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
            // 보스의 체력이 70%이하가 되면
            if(bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)
            {
                // 원 발사 형태 중지
                bossWeapon.StopFire(AtkType.Circle);
                ChangeState(BossState.Phase2);
            } 

            // 보스 체력이 30%이하가 되면
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
        // 플레이어 위치로 단일 공격
        bossWeapon.StartFire(AtkType.FireToPlayer);

        // 처음 이동 방향을 오른쪽으로
        Vector3 direction = Vector3.right;
        movement.Move(direction);

        while (true)
        {
            // 좌우 이동 중 양쪽 끝에 도달하게 되면 방향을 반대로
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
        // 원 형태 공격 시작
        bossWeapon.StartFire(AtkType.Circle);
        // 플레이어 위치로 단일 공격
        bossWeapon.StartFire(AtkType.FireToPlayer);

        // 처음 이동 방향을 오른쪽으로
        Vector3 direction = Vector3.right;
        movement.Move(direction);

        while (true)
        {
            // 좌우 이동 중 양쪽 끝에 도달하게 되면 방향을 반대로
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
        // 보스 파괴 이펙트 생성
        Instantiate(bossExplosionPrefab, transform.position, Quaternion.identity);
        // 보스 오브젝트 삭제
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
