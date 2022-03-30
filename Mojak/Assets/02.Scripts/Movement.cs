using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 8f;
    [SerializeField]
    private Vector3 moveDiretion = Vector3.zero;

    void Update()
    {
        transform.position += moveDiretion * moveSpeed * Time.deltaTime;
    }

    public void Move(Vector3 direction)
    {
        moveDiretion = direction;
    }
}
