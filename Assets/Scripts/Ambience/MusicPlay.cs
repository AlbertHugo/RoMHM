using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicClip;  // A música que você quer tocar
    private AudioSource audioSource;  // O componente AudioSource que irá tocar a música
    public AudioMixerGroup sfxMixerGroup;

    void Awake()
    {
    RepeatableCode.sfxMixerGroup = sfxMixerGroup;
    }
    void Start()
    {
        // Obtém o componente AudioSource anexado a este GameObject
        audioSource = GetComponent<AudioSource>();

        // Se o AudioSource existir e a música estiver atribuída, toca a música
        if (audioSource != null && musicClip != null)
        {
            audioSource.clip = musicClip;  // Atribui a música ao AudioSource
            audioSource.loop = true;  // Se você quiser que a música fique em loop, ative
            audioSource.Play();  // Começa a tocar a música
        }
        else
        {
            Debug.LogError("AudioSource ou MusicClip não atribuídos!");//Aviso se não tiver arquivo
        }
    }
}
//coloque tanto o .mp3 quanto o script no objeto