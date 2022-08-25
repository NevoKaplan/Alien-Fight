using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < sounds.Length; i++) 
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
        }
    }

    // Update is called once per frame
    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null) 
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.PlayOneShot(s.clip);
    }

    public static void playSound(string name)
    {
        if (instance == null) 
        {
            Debug.LogWarning("No AudioManager");
            return;
        }
        instance.play(name);
    }

    private void Start()
    {
        play("Theme");
    }
}
