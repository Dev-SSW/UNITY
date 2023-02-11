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
    //�̱��� , 1���� ���� ��Ű�� ��

    #region singleton
    void Awake()   //��ü ������ ���� ����
    {
        if (instance == null)
        {
            instance = this; // �����⿡ �ڱ� �ڽ��� ����
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
                Debug.Log("��� ���� AudioSource�� ������Դϴ�.");
                return;
            }
        }
        Debug.Log(_name + "���尡 SoundManger�� ��ϵ��� �ʾҽ��ϴ�");
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
        Debug.Log("��� ����" + name + " ���尡 �����ϴ�");
    }
}
