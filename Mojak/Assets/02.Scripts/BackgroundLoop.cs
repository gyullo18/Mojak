using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    //배경 세로
    private float height;

    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        height = backgroundCollider.size.y - 0.54f;
    }

    void Update()
    {
        if (transform.position.y <= -height)
        {
            Reposition();
        }
    }

    void Reposition()
    {
        Vector2 offset = new Vector2(0, height * 2f);
        transform.position = (Vector2)transform.position + offset;
    }
}
