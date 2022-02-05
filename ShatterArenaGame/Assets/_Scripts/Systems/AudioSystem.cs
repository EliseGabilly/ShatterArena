using System.Collections.Generic;
using UnityEngine;

/// Audio system with 3D sound
public class AudioSystem : StaticInstance<AudioSystem> {

    #region Variable
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;

    [SerializeField]
    private List<AudioClip> music;
    private int musicIndex = 0;
    [SerializeField]
    private AudioClip hit;
    #endregion

    private void Start() {
        _musicSource.clip = music[musicIndex];
        _musicSource.Play();
        TurnMusicOn(Player.Instance.isSoundOn);
    }

    private void Update() {
        if (!_musicSource.isPlaying) {
            PlayNextSong();
        }
    }

    public void TurnMusicOn(bool isOn) {
        _musicSource.volume = isOn ? 0.5f : 0;
        _soundsSource.volume = isOn ? 0.5f : 0;
    }

    private void PlayNextSong() {
        musicIndex = (musicIndex + 1) % music.Count;
        _musicSource.clip = music[musicIndex];
        _musicSource.Play();
    }

    public void PlayMusic(AudioClip clip) {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1) {
        _soundsSource.transform.position = pos;
        PlaySound(clip, vol);
    }

    public void PlaySound(AudioClip clip, float vol = 1) {
        _soundsSource.PlayOneShot(clip, vol);
    }

    public void PlayHit() {
        PlaySound(hit);
    }
}