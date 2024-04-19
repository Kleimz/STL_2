using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum ActiveState
{
    combat,
    cell,
    adventure,
}
public class Music : MonoBehaviour
{
    public static Music singleton;

    AudioSource worldMusic;
    public ActiveState musicState;

    public AudioClip combatMusic;
    public AudioClip cellMusic;
    public AudioClip adventureMusic;

    Dictionary<string,AudioClip> music;
    // Start is called before the first frame update
    void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
        DontDestroyOnLoad(gameObject);

        // Initialize the music dictionary
        music = new Dictionary<string, AudioClip>();

        music.Add(ActiveState.combat.ToString(), combatMusic);
        music.Add(ActiveState.cell.ToString(), cellMusic);
        music.Add(ActiveState.adventure.ToString(), adventureMusic);

        worldMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void ChangeState(ActiveState newState)
    {
        musicState = newState;
        PlayMusic(music[newState.ToString()]);
    }
    void PlayMusic(AudioClip music)
    {
        worldMusic.clip = music;
        worldMusic.Play();
    }
}
