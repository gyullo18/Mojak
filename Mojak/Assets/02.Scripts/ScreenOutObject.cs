using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOutObject : MonoBehaviour
{
    [SerializeField]
    private StageSize stageSize;
    private float destroyObj = 3.0f;

    private void LateUpdate()
    {
        if (transform.position.y < stageSize.LimitMin.y - destroyObj ||
            transform.position.y > stageSize.LimitMax.y + destroyObj ||
            transform.position.x < stageSize.LimitMin.x - destroyObj ||
            transform.position.x > stageSize.LimitMax.x + destroyObj)
        {
            Destroy(gameObject);
        }
    }
}
