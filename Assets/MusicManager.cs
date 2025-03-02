using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musicTracks;
    private int currentTrackIndex = 0;

    void Start()
    {
        if (musicTracks.Length > 0)
        {
            musicSource.clip = musicTracks[currentTrackIndex];
            musicSource.Play();
        }
    }

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        musicSource.clip = musicTracks[currentTrackIndex];
        musicSource.Play();
    }
}
