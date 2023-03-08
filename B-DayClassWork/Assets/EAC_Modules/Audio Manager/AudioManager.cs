using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    //Public Members

    public AudioSource NarrationSource;
    public AudioSource EffectSource;
    public AudioSource MusicSource;
	public AudioSource AmbianceSource;

    public float narriationDelay = 0.5f;

    public enum AudioSourceType
    {
        NARRATION,
        EFFECT,
        MUSIC,
		AMBIANCE
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupAudioSources();
    }

    private void SetupAudioSources()
    {
        foreach (AudioSource source in gameObject.GetComponents(typeof(AudioSource)))
        {
            if (NarrationSource == null)
                NarrationSource = source;
            else if (EffectSource == null)
                EffectSource = source;
            else if (MusicSource == null)
                MusicSource = source;
			else if (AmbianceSource == null)
				AmbianceSource = source;
        }

    }

    public void PlayAudio(AudioSourceType type, AudioClip clip)
    {
        switch (type)
        {
            case AudioSourceType.NARRATION:
                PlayNarrationAudio(clip);
                break;

            case AudioSourceType.EFFECT:
                PlayEffectAudio(clip);
                break;

            case AudioSourceType.MUSIC:
                PlayMusicAudio(clip);
                break;
				
			case AudioSourceType.AMBIANCE:
				PlayAmbianceAudio(clip);
				break;

            default:
                break;
        }
    }

    private void PlayNarrationAudio(AudioClip clip)
    {
        NarrationSource.Stop();
        NarrationSource.clip = clip;
        NarrationSource.PlayDelayed(narriationDelay);
    }

    private void PlayEffectAudio(AudioClip clip)
    {
        EffectSource.Stop();
        EffectSource.clip = clip;
        EffectSource.Play();
    }

    private void PlayMusicAudio(AudioClip clip)
    {
        MusicSource.Stop();
        MusicSource.clip = clip;
        MusicSource.Play();
    }
	
	private void PlayAmbianceAudio(AudioClip clip)
    {
        AmbianceSource.Stop();
        AmbianceSource.clip = clip;
        AmbianceSource.Play();
    }

}
