using System;
using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public Sound[] sfxSound;
    public AudioSource sfxSource;


    public void SfxPlay(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);
        if(s == null)   
        {
            return;
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
