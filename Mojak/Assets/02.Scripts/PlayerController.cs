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

    // ü��
    [SerializeField]
    private float maxHP = 10;
    private float currentHP;
    private SpriteRenderer spriteRenderer;

    // ü�� ���� ���� ����
    public float MaxHP => maxHP;
    public float CurrentHP
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }
   

    // ��ź ���� Ű
    [SerializeField]
    private KeyCode keyCodeBoom = KeyCode.Z;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        weapon = GetComponent<Weapon>();

        // ü��
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
            Debug.Log("�߻�");
        }
        else if (Input.GetKeyUp(keyCodeAtt))
        {
            weapon.StopFiring();
        }

        // ��ź ����
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

    //�ǰݽ�
    public void DamageHit(float damage)
    {
        // ü�� -damage��ŭ ����
        currentHP -= damage;

        StopCoroutine("HitAnimation");
        StartCoroutine("HitAnimation");

        // ü�� 0 ���� �÷��̾� ���
        if (currentHP <= 0)
        {
            Die();
        }
        
    }

    private IEnumerator HitAnimation()
    {
        // �÷��̾� ���� ����
        spriteRenderer.color = Color.red;
        //0.1�� ���
        yield return new WaitForSeconds(0.1f);
        // ���� ��������
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        isDead = true;
        GameManager.instance.OnPlayerDead();
    }
}
