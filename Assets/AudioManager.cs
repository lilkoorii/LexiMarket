using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Синглтон
    public AudioMixer audioMixer; // Ссылка на AudioMixer
    private AudioClip backgroundMusic; // Фоновая музыка
    private AudioSource audioSource; // Источник звука

    void Awake()
    {
        // Проверка на существование экземпляра
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать объект при загрузке новой сцены
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дубликаты
        }
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Добавляем компонент AudioSource
        audioSource.clip = backgroundMusic; // Устанавливаем фоновую музыку
        audioSource.loop = true; // Зацикливаем музыку
        audioSource.Play(); // Начинаем воспроизведение
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        float dbValue = volume > 0 ? Mathf.Log10(volume) * 20 : -80f; // Избегаем бесконечности
        audioMixer.SetFloat("volume", dbValue); // Устанавливаем громкость
    }
}