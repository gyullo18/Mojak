using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private StageSize stageSize;
    private Movement movement;

    [SerializeField]
    private KeyCode keyCodeAtt = KeyCode.Space;
    private Weapon weapon;
    private void Awake()
    {
        movement = GetComponent<Movement>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement.Move(new Vector3(x, y, 0));

        if (Input.GetKeyDown(keyCodeAtt))
        {
            weapon.StartFiring();
        }
        else if (Input.GetKeyUp(keyCodeAtt))
        {
            weapon.StopFiring();
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageSize.LimitMin.x, stageSize.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageSize.LimitMin.y, stageSize.LimitMax.y));
    }
}
