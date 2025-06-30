using UnityEngine;
using UnityEngine.Audio;

public class EndMusic : MonoBehaviour
{
    public AudioClip musicClip;  // A m�sica que voc� quer tocar
    private AudioSource audioSource;  // O componente AudioSource que ir� tocar a m�sica
    void Start()
    {
        // Obt�m o componente AudioSource anexado a este GameObject
        audioSource = GetComponent<AudioSource>();

        // Se o AudioSource existir e a m�sica estiver atribu�da, toca a m�sica
        if (audioSource != null && musicClip != null)
        {
            audioSource.clip = musicClip;  // Atribui a m�sica ao AudioSource
            audioSource.loop = false;  // Se voc� quiser que a m�sica fique em loop, ative
            audioSource.Play();  // Come�a a tocar a m�sica
        }
        else
        {
            Debug.LogError("AudioSource ou MusicClip n�o atribu�dos!");//Aviso se n�o tiver arquivo
        }
    }
}
//coloque tanto o audio source com o .mp3 quanto o script no objeto