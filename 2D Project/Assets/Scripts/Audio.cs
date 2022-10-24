using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio
{
    #region Private_Variables

    private AudioSource sourceSFX;

    private AudioSource sourceMusic;

    private AudioSource sourceRandomPitchSFX;

    private float musicVolume = 1f;

    private float sfxVolume = 1f;

    [SerializeField]
    private AudioClip[] sounds;

    [SerializeField]
    private AudioClip defaulftClip;

    [SerializeField]
    private AudioClip menuMusic;

    [SerializeField]
    private AudioClip gameMusic;

    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = value;
            SourceMusic.volume = musicVolume;
        }
    }
    public float SfxVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            sfxVolume = value;
            SourceSFX.volume = sfxVolume;
            SourceRandomPitchSFX.volume = sfxVolume;
        }
    }
    public AudioSource SourceSFX { get => sourceSFX; set => sourceSFX = value; }
    public AudioSource SourceMusic { get => sourceMusic; set => sourceMusic = value; }
    public AudioSource SourceRandomPitchSFX { get => sourceRandomPitchSFX; set => sourceRandomPitchSFX = value; }

    #endregion

    ///<summary>
    ///����� ����� � �������
    ///</summary>
    ///<param name="clipName">��� �����</param>
    ///<returns>����. ���� ���� �� ������, ���������� �������� ���������� defaultClip</returns>
    private AudioClip GetSound(string clipName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == clipName) 
            {
                return sounds[i];
            }
        }
        Debug.LogError("Can not find clip" + clipName);
        return defaulftClip;
    }

    ///<summary>
    ///��������������� ����� �� �������
    ///</summary>
    ///<param name="clipName">��� �����</param>
    public void PlaySound(string clipName)
    {
        SourceSFX.PlayOneShot(GetSound(clipName), SfxVolume);
    }

    ///<summary>
    ///��������������� ����� �� �������� ��������
    ///</summary>
    ///<param name="clipName">��� �����</param>
    public void PlaySoundRandomPitch(string clipName) 
    {
        SourceRandomPitchSFX.pitch = Random.Range(0.7f, 1.3f);
        SourceRandomPitchSFX.PlayOneShot(GetSound(clipName), SfxVolume);
    }

    ///<summary>
    ///��������������� ������
    ///</summary>
    ///<param name="menu">��� �������� ����</param>
    public void PlayMusic(bool menu)
    {
        if (menu)
        {
            SourceMusic.clip = menuMusic;
        }
        else
        {
            SourceMusic.clip = gameMusic;
        }

        sourceMusic.volume = MusicVolume;
        sourceMusic.loop = true;
        sourceMusic.Play();

    }
}
