using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType { Stage =0, Boss }

public class AudioEffect : MonoBehaviour
{
    // ��������BGM
    [SerializeField]
    private AudioClip[] bgmClips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void ChangeBgm(BGMType index)
    {
        // ���� ��� ���� ������� ����
        audioSource.Stop();
        // ������� ��Ͽ��� Index��°�� �������� ��ü
        audioSource.clip = bgmClips[(int)index];
        audioSource.Play();
    }
}
