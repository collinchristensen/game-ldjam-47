using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;

    public float lowPitchRange = .98f;
    public float highPitchRange = 1.02f;


    public static AudioManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                instance = go.AddComponent<AudioManager>();
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingleSound(AudioClip clip)
    {
        efxSource.clip = clip;

        efxSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;

        musicSource.Play();
    }


    public void RandomizeSound(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;

        efxSource.clip = clips[randomIndex];

        efxSource.Play();
    }
}