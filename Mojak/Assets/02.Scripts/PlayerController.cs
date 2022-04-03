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

    // 방향키 설정(9개 방향) - 버튼의 어디를 눌렀는지.
    public bool[] joyControl;
    // 방향키 버튼을 눌렀는지
    public bool isControl;
    // A버튼을 눌렀는지
    public bool isButtonA;
    // B버튼을 눌렀는지
    public bool isButtonB;

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
        //    Debug.Log("발사");
        //}
        ////else if (Input.GetKeyUp(keyCodeAtt))
        //else if (!isButtonA)
        //{
        //    weapon.StopFiring();
        //}

        //if (!isButtonB) return;

        // 폭탄 생성
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

    // A(Attack), B(Boom) 버튼
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
