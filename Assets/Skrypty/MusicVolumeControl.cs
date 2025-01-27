using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Suwak do sterowania głośnością
    private AudioSource audioSource; // Komponent AudioSource z muzyką

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>(); // Znajduje AudioSource w scenie
        if (audioSource != null)
        {
            volumeSlider.value = audioSource.volume; // Ustawienie wartości suwaka na aktualną głośność
            volumeSlider.onValueChanged.AddListener(SetVolume); // Nasłuchiwanie zmian suwaka
        }
        else
        {
            Debug.LogError("Nie znaleziono komponentu AudioSource w scenie!");
        }
    }

    private void SetVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value; // Ustawianie głośności
        }
    }
}
