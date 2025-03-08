using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    private AudioSource musicSource;
    private AudioSource sfxSource;

    [Header("Background Music List")]
    public List<AudioClip> backgroundMusicList = new List<AudioClip>();

    [Header("Sound Effects List")]
    public List<SFXClip> soundEffects = new List<SFXClip>();

    private Dictionary<SFX, AudioClip> sfxDictionary = new Dictionary<SFX, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSource bileşenlerini ekleyelim
            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            musicSource.loop = true;
            musicSource.playOnAwake = false;

            // Enum ile Dictionary eşleme
            foreach (var sfx in soundEffects)
            {
                if (!sfxDictionary.ContainsKey(sfx.sfxType))
                {
                    sfxDictionary.Add(sfx.sfxType, sfx.clip);
                }
            }

            // Rastgele bir müzik çal
            PlayRandomMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🎵 Rastgele background müzik çal
    public void PlayRandomMusic()
    {
        if (backgroundMusicList.Count == 0) return;

        AudioClip randomClip = backgroundMusicList[Random.Range(0, backgroundMusicList.Count)];
        musicSource.clip = randomClip;
        musicSource.Play();
    }

    // 🎵 Belirli bir background müziğini çal
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    // 🎧 Enum ile ses efekti çal
    public void PlaySFX(SFX sfxType, float volume = 1f)
    {
        if (sfxDictionary.TryGetValue(sfxType, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxType}' bulunamadı!");
        }
    }

    // 🔊 Müzik ses seviyesini ayarla
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // 🎚️ Ses efekti seviyesini ayarla
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // 🎼 Müzik durdur
    public void StopMusic()
    {
        musicSource.Stop();
    }
}

// 🎵 Enum ile ses efektleri
[System.Serializable]
public enum SFX
{
    Paper,
    Footstep,
    ButtonClick,
    Stamp
}

// 🎶 Ses efektleri için özel yapı
[System.Serializable]
public class SFXClip
{
    public SFX sfxType;
    public AudioClip clip;
}
