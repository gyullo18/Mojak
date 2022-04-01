using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoomCounter : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    private Text boomCount;
    // Start is called before the first frame update
    void Awake()
    {
        boomCount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        boomCount.text = " x " + weapon.BoomCount;
    }
}
