using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState {MoveToAppear = 0, }
public class Boss : MonoBehaviour
{
    [SerializeField]
    private float bossAppear = 2.5f;
    private BossState bossState = BossState.MoveToAppear;
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
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
            }
            yield return null;
        }
    }
}
