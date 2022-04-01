using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType { Stage =0, Boss }

public class AudioEffect : MonoBehaviour
{
    // 보스생성BGM
    [SerializeField]
    private AudioClip[] bgmClips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void ChangeBgm(BGMType index)
    {
        // 현재 재생 중인 배경음악 정지
        audioSource.Stop();
        // 배경음악 목록에서 Index번째의 음악으로 교체
        audioSource.clip = bgmClips[(int)index];
        audioSource.Play();
    }
}
