using System.Collections;
using System.Linq;
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
    public List<Audio_Clip> SoundEffect_Clip_List;
    public List<Audio_ClipCategory> NPC_audioClip;

    private Audio_Clip previous_music;
    private Coroutine BG_music;

    public void Start()
    {
        soudEffect_AudioSource = new List<AudioSource>();
        PlayBG_Music();
    }


    // เล่นเพลงใน playlist แบบ Random
    private IEnumerator PlayRandomBG_Music()
    {
        while (true)
        {
            if (!music_AudioSource.isPlaying)
            {
                var audioClip = music_Clip_List[Random.Range(0, music_Clip_List.Count)];
                if (previous_music.clipName != audioClip.clipName)
                {
                    previous_music = audioClip;
                    _Play(music_AudioSource, audioClip.clip);
                }
            }

            yield return null;
        }
    }


    // เล่นเพลงพื้นหลัง
    public void PlayBG_Music()
    {
        BG_music = StartCoroutine(PlayRandomBG_Music());
    }

    // เล่นหยุดเล่นเพลง
    public void StopBG_Music()
    {
        StopCoroutine(BG_music);
        music_AudioSource.Stop();
    }


    // ฟังชั้นสำหรับเล่น clip ใน AudioSouce ที่ต้องการ
    public void _Play(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    // ฟังชั้นสำหรับเล่น SoundEffect เขียนไว้เผื่อมีกรณีที่เสียงEffect ต้องเล่นซ้อนกันหลายๆอัน เลยใช้ logic คล้ายๆ object pool หาก Souce ในลิสทั้งหมดเล่นอยู่หรือยังไม่มีก็จะสร้าง AudioSource เพิ่มแต่ถ้ามี AudioSouce ที่ไม่ได้เล่นเสียงอะไรก็จะถูกเอามาใช้เล่น
    public void PlaySoundEffect(string clipName, float volume)
    {
        var clip = SoundEffect_Clip_List.Where(x => x.clipName == clipName).SingleOrDefault().clip;

        foreach (var x in soudEffect_AudioSource)
        {
            if (!x.isPlaying)
            {
                x.clip = clip;
                x.volume = volume;
                x.Play();

                return;
            }
        }

        var temp_AudioSource = this.gameObject.AddComponent<AudioSource>();
        temp_AudioSource.clip = clip;
        temp_AudioSource.volume = volume;
        temp_AudioSource.Play();

        soudEffect_AudioSource.Add(temp_AudioSource);
    }


    // ฟั่งชั่นสุ่มเสียง NPC ตาม Category ที่แยกไว้เช่น Welcome ก็จะเป็นเสียงพูดตอนเปิดเกม
    public IEnumerator PlayRandom_IN_NPC_Category(string categoryName)
    {
        RandomPlay_by_Category(NPC_audioClip, npcSpeak_AudioSource, categoryName);

        while (npcSpeak_AudioSource.isPlaying)
        {
            yield return null;
        }
    }

    // Random เสียงที่อยู่ใน Category ที่ต้องการ
    public void RandomPlay_by_Category(List<Audio_ClipCategory> audio_list, AudioSource audioSource, string categoryName)
    {
        var clip_in_category = audio_list.Where(x => x.Category_Name == categoryName).SingleOrDefault().clip_list;
        var select_Clip = clip_in_category[Random.Range(0, clip_in_category.Count)];
        audioSource.clip = select_Clip.clip;
        audioSource.Play();
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
