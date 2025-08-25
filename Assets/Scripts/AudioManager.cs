using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SfxId
{
    Coin,
    Button,
    Jump,
    Hit,
    Win,
    Lose
}

[System.Serializable]
public struct SfxEntry
{
    public SfxId id;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private AudioSource sfxSource;   

    [Header("SFX Library")]
    [SerializeField] private SfxEntry[] sfxLibrary;

    private Dictionary<SfxId, AudioClip> sfxMap;

    private const string PP_MUSIC = "PP_MUSIC_LINEAR";
    private const string PP_SFX = "PP_SFX_LINEAR";


    private float musicLinear = 0.8f;
    private float sfxLinear = 0.8f;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        sfxMap = new Dictionary<SfxId, AudioClip>();
        if (sfxLibrary != null)
        {
            foreach (var e in sfxLibrary)
            {
                if (!sfxMap.ContainsKey(e.id) && e.clip != null)
                    sfxMap.Add(e.id, e.clip);
            }
        }

        // Load volume
        musicLinear = PlayerPrefs.GetFloat(PP_MUSIC, 0.8f);
        sfxLinear = PlayerPrefs.GetFloat(PP_SFX, 0.8f);
        ApplyMusicVolume();
        ApplySFXVolume();
    }


    public void SetMusicVolume(float linear01)
    {
        musicLinear = Mathf.Clamp01(linear01);
        ApplyMusicVolume();
        PlayerPrefs.SetFloat(PP_MUSIC, musicLinear);
    }

    public void SetSFXVolume(float linear01)
    {
        sfxLinear = Mathf.Clamp01(linear01);
        ApplySFXVolume();
        PlayerPrefs.SetFloat(PP_SFX, sfxLinear);
    }

    public float GetMusicVolume() => musicLinear;
    public float GetSFXVolume() => sfxLinear;

    private void ApplyMusicVolume()
    {
        if (musicSource) musicSource.volume = musicLinear;
    }

    private void ApplySFXVolume()
    {
        if (sfxSource) sfxSource.volume = sfxLinear;
    }


    public void PlayMusic(AudioClip clip, bool loop = true, float fadeTime = 0.5f)
    {
        if (clip == null || musicSource == null) return;
        StopAllCoroutines();
        StartCoroutine(Co_FadeInMusic(clip, loop, fadeTime));
    }

    public void StopMusic(float fadeTime = 0.3f)
    {
        if (musicSource == null) return;
        StopAllCoroutines();
        StartCoroutine(Co_FadeOutMusic(fadeTime));
    }

    private IEnumerator Co_FadeInMusic(AudioClip clip, bool loop, float fadeTime)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;

        float t = 0f;
        musicSource.volume = 0f;
        musicSource.Play();

        if (fadeTime <= 0f) { musicSource.volume = musicLinear; yield break; }

        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(0f, musicLinear, t / fadeTime);
            yield return null;
        }
        musicSource.volume = musicLinear;
    }

    private IEnumerator Co_FadeOutMusic(float fadeTime)
    {
        if (!musicSource.isPlaying) yield break;

        float start = musicSource.volume;
        float t = 0f;

        if (fadeTime <= 0f)
        {
            musicSource.Stop();
            musicSource.volume = musicLinear;
            yield break;
        }

        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(start, 0f, t / fadeTime);
            yield return null;
        }
        musicSource.Stop();
        musicSource.volume = musicLinear;
    }


    public void PlaySFX(SfxId id, float volumeScale = 1f, float pitch = 1f)
    {
        if (sfxSource == null) return;
        if (!sfxMap.TryGetValue(id, out var clip) || clip == null) return;

        float originalPitch = sfxSource.pitch;
        sfxSource.pitch = Mathf.Clamp(pitch, 0.5f, 2f);


        sfxSource.PlayOneShot(clip, Mathf.Clamp01(volumeScale));

        sfxSource.pitch = originalPitch;
    }


    public void PlaySFXAt(SfxId id, Vector3 position, float volumeScale = 1f)
    {
        if (!sfxMap.TryGetValue(id, out var clip) || clip == null) return;

        // Kết hợp volume slider SFX
        float finalVol = Mathf.Clamp01(sfxLinear * volumeScale);
        if (finalVol <= 0f) return;

        AudioSource.PlayClipAtPoint(clip, position, finalVol);
    }


    public void PlaySFX(AudioClip clip, float volumeScale = 1f, float pitch = 1f)
    {
        if (clip == null || sfxSource == null) return;
        float originalPitch = sfxSource.pitch;
        sfxSource.pitch = Mathf.Clamp(pitch, 0.5f, 2f);
        sfxSource.PlayOneShot(clip, Mathf.Clamp01(volumeScale));
        sfxSource.pitch = originalPitch;
    }
}
