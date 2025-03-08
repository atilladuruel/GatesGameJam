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
    public List<SoundEffect> soundEffects = new List<SoundEffect>();

    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

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

            // SFX Dictionary'sini oluştur
            foreach (var sfx in soundEffects)
            {
                if (!sfxDictionary.ContainsKey(sfx.name))
                {
                    sfxDictionary.Add(sfx.name, sfx.clip);
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

    // 🎧 İsme göre ses efekti çal
    public void PlaySFX(string sfxName, float volume = 1f)
    {
        if (sfxDictionary.TryGetValue(sfxName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' bulunamadı!");
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

// 🎵 Ses efektleri için özel sınıf
[System.Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip clip;
}
