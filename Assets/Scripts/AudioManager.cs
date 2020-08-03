using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Массив со звуками
    /// </summary>
    public AudioClip[] clips;

    /// <summary>
    /// Источник аудио
    /// </summary>
    private AudioSource audioSource;

    private void Start()
    {
        //Получаем компоненты
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Проигрывает аудио
    /// </summary>
    /// <param name="id">Айди клипа (от 0 до clips.length)</param>
    public void PlaySound(int id)
    {
        audioSource.Stop();
        audioSource.clip = clips[id];
        audioSource.Play();
    }
}
