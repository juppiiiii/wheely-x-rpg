using UnityEngine;

public class backGroundMusic : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true; // BGM �ݺ� ���
            bgmSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
