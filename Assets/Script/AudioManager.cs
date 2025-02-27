using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------------Audio Source--------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------------Audio Clip--------------")]
    public AudioClip background; //배경음 오브젝트
    public AudioClip Sword_SFX; //주인공오브젝트 칼휘두르는 소리
    public AudioClip playerDown; //플레이어 죽는소리
    public AudioClip mosterA_Down; //몬스터A 죽는소리
    public AudioClip mosterB_Down; //몬스터B 죽는소리
    public AudioClip mosterC_Down; //몬스터C 죽는소리
    public AudioClip mosterBoss_Down; //보스몬스터 죽는소리

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip); 
    }
}
