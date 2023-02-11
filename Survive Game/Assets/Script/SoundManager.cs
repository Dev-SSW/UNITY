using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{

    static public SoundManager instance;
    //싱글턴 , 1개를 유지 시키는 것

    #region singleton
    void Awake()   //객체 생성시 최초 실행
    {
        if (instance == null)
        {
            instance = this; // 껍데기에 자기 자신을 넣음
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);

    }
    #endregion singleton
    public AudioSource[] audiosourceEffects;
    public AudioSource audioSourceBgm;

    public string[] PlaySoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    void Start()
    {
        PlaySoundName = new string[audiosourceEffects.Length];
    }

    public void PlaySE(string _name) 
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                for (int j = 0; j < audiosourceEffects.Length; j++)
                {
                    if (!audiosourceEffects[j].isPlaying)
                    {
                        PlaySoundName[j] = effectSounds[i].name;
                        audiosourceEffects[j].clip = effectSounds[i].clip;
                        audiosourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManger에 등록되지 않았습니다");
    }

    public void StopAllSE() {
        for (int i = 0; i < audiosourceEffects.Length; i++)
        {
            audiosourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audiosourceEffects.Length; i++)
        {
            if (PlaySoundName[i] == _name)
            {
                audiosourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생 중인" + name + " 사운드가 없습니다");
    }
}
