using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [Header("AudioSource")]
    public AudioSource music_AudioSource;
    public AudioSource npcSpeak_AudioSource;
    List<AudioSource> soudEffect_AudioSource;
    [Header("Audio Clip Section")]
    public List<Audio_Clip> music_Clip_List;
    public List<Audio_ClipCategory> NPC_audioClip;


    private bool playBG_music;
    private Audio_Clip previous_music;


    public void Start()
    {
        soudEffect_AudioSource = new List<AudioSource>();
    }


    public void Update()
     {
        if (!music_AudioSource.isPlaying) 
        {
            RandomBG_Music();
        }
     }

    private void RandomBG_Music() 
    {
        
        var audioClip = music_Clip_List[Random.Range(0, music_Clip_List.Count)];
        Debug.Log(previous_music.clipName != audioClip.clipName);
        if (previous_music.clipName != audioClip.clipName) 
        {
            previous_music = audioClip;
            _Play(music_AudioSource, audioClip.clip);
        }


    }

    public void _Play(AudioSource source , AudioClip clip) 
    {
        source.clip = clip;
        source.Play();
    }
    public void PlaySoundEffect(AudioClip clip)
    {
      
    }

    public IEnumerator IE_PlaySoundEffect() 
    {

        yield return null;
    }


    [System.Serializable]
    public struct Audio_Clip 
    {
        public string clipName;
        public AudioClip clip;
    
    }

    [System.Serializable]
    public struct Audio_ClipCategory
    {
        public string Category_Name;
        public List<Audio_Clip> clip_list;

    }


}
