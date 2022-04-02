using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead = false;

    [SerializeField]
    private StageSize stageSize;
    private Movement movement;

    [SerializeField]
    private KeyCode keyCodeAtt = KeyCode.Space;
    private Weapon weapon;

    // 체력
    [SerializeField]
    private float maxHP = 10;
    private float currentHP;
    private SpriteRenderer spriteRenderer;

    // 체력 관련 변수 접근
    public float MaxHP => maxHP;
    public float CurrentHP
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }
   

    // 폭탄 생성 키
    [SerializeField]
    private KeyCode keyCodeBoom = KeyCode.Z;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        weapon = GetComponent<Weapon>();

        // 체력
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDead) return;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement.Move(new Vector3(x, y, 0));

        if (Input.GetKeyDown(keyCodeAtt))
        {
            weapon.StartFiring();
            Debug.Log("발사");
        }
        else if (Input.GetKeyUp(keyCodeAtt))
        {
            weapon.StopFiring();
        }

        // 폭탄 생성
        if( Input.GetKeyDown(keyCodeBoom))
        {
            weapon.StartBoom();
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageSize.LimitMin.x, stageSize.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageSize.LimitMin.y, stageSize.LimitMax.y));
    }

    //피격시
    public void DamageHit(float damage)
    {
        // 체력 -damage만큼 감소
        currentHP -= damage;

        StopCoroutine("HitAnimation");
        StartCoroutine("HitAnimation");

        // 체력 0 이하 플레이어 사망
        if (currentHP <= 0)
        {
            Die();
        }
        
    }

    private IEnumerator HitAnimation()
    {
        // 플레이어 색상 변경
        spriteRenderer.color = Color.red;
        //0.1초 대기
        yield return new WaitForSeconds(0.1f);
        // 원래 색상으로
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        isDead = true;
        GameManager.instance.OnPlayerDead();
    }
}
