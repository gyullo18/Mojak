using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead = false;

    [SerializeField]
    private StageSize stageSize;
    private Movement movement;

    //[SerializeField]
    //private KeyCode keyCodeAtt = KeyCode.Space;
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

    // ����Ű ����(9�� ����) - ��ư�� ��� ��������.
    public bool[] joyControl;
    // ����Ű ��ư�� ��������
    public bool isControl;
    // A��ư�� ��������
    public bool isButtonA;
    // B��ư�� ��������
    public bool isButtonB;

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

        if (joyControl[0]) { x = -1; y = 1; }
        if (joyControl[1]) { x = 0; y = 1; }
        if (joyControl[2]) { x = 1; y = 1; }
        if (joyControl[3]) { x = -1; y = 0; }
        if (joyControl[4]) { x = 0; y = 0; }
        if (joyControl[5]) { x = 1; y = 0; }
        if (joyControl[6]) { x = -1; y = -1; }
        if (joyControl[7]) { x = 0; y = -1; }
        if (joyControl[8]) { x = 1; y = -1; }

        if (!isControl) x = 0;
        if (!isControl) y = 0;

        movement.Move(new Vector3(x, y, 0));

        //if (!isButtonA) return;

        //if (Input.GetKeyDown(keyCodeAtt))
        //if (isButtonA)
        //{
        //    weapon.StartFiring();
        //    Debug.Log("�߻�");
        //}
        ////else if (Input.GetKeyUp(keyCodeAtt))
        //else if (!isButtonA)
        //{
        //    weapon.StopFiring();
        //}

        //if (!isButtonB) return;

        // ��ź ����
        //if ( Input.GetKeyDown(keyCodeBoom))
    //    if (isButtonB)
    //    {
    //        weapon.StartBoom();
    //    }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageSize.LimitMin.x, stageSize.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageSize.LimitMin.y, stageSize.LimitMax.y));
    }

    public void JoyPanel(int type)
    {
        for (int i =0; i < 9; i++)
        {
            joyControl[i] = i == type;
        }
    }
    public void JoyDown()
    {
        isControl = true;
    }
    public void JoyUp()
    {
        isControl = false;
    }

    // A(Attack), B(Boom) ��ư
    public void ButtonADown()
    {
        isButtonA = true;
        weapon.StartFiring();
    }

    public void ButtonAUp()
    {
        isButtonA = false;
        weapon.StopFiring();
    }

    public void ButtonBClick() 
    {
        isButtonB = true;
        weapon.StartBoom();
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
