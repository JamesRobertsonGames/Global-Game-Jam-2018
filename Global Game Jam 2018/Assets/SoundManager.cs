using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public AudioClip[] squeaks;
    public AudioClip theme;
    public AudioClip win;
    public AudioClip lose;

    public AudioSource themeSource;
    public AudioSource generalSource;

    private void Start()
    {
        themeSource.clip = theme;
        themeSource.loop = true;
        PlayThemeLoop();
        PlayRandomSqueak(); // play a cheeky squeak right away
    }

    public void PlayRandomSqueak()
    {
        if (squeaks != null && squeaks.Length > 0)
        {
            int random = Random.Range(0, squeaks.Length);
            generalSource.PlayOneShot(squeaks[random]);
        }
    }

    public void PlayThemeLoop()
    {
        themeSource.Play();
    }

    public void StopThemeLoop()
    {
        themeSource.Stop();
    }

    public void PlayWin()
    {
        generalSource.PlayOneShot(win);
    }

    public void PlayLose()
    {
        generalSource.PlayOneShot(lose);
    }
}
