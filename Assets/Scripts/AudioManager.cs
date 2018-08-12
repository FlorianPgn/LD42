using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private float _volume = 1f;

    private AudioSource[] _musicSources;
    private int _activeMusicSourceIndex;

    public static AudioManager instance;

    // Use this for initialization
    void Awake()
    {
        instance = this;

        _musicSources = new AudioSource[2];
        for (int i = 0; i < _musicSources.Length; i++)
        {
            GameObject newMusicSource = new GameObject("Music source " + (i + 1));
            _musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }
    }


    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        _activeMusicSourceIndex = 1 - _activeMusicSourceIndex;
        _musicSources[_activeMusicSourceIndex].clip = clip;
        _musicSources[_activeMusicSourceIndex].Play();
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, _volume);
    }

    private IEnumerator AnimatedMusicCrossFade(float duration)
    {
        float percent = 0;
        while(percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            _musicSources[_activeMusicSourceIndex].volume = Mathf.Lerp(0, _volume, percent);
            _musicSources[1-_activeMusicSourceIndex].volume = Mathf.Lerp(_volume, 0, percent);
            yield return null;
        }
    }
}
