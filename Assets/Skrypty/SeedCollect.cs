using UnityEngine;
using System.Collections.Generic;

public class SeedCollect : MonoBehaviour
{
    private Dictionary<string, int> seeds = new Dictionary<string, int>();

    void Start()
    {
        // Odczytanie liczby nasion z PlayerPrefs
        seeds["Seed1"] = PlayerPrefs.GetInt("Seed1", 1);
        seeds["Seed2"] = PlayerPrefs.GetInt("Seed2", 1);
        seeds["Seed3"] = PlayerPrefs.GetInt("Seed3", 1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name); // Debugowanie
        if (other.gameObject.CompareTag("Seed1") || other.gameObject.CompareTag("Seed2") || other.gameObject.CompareTag("Seed3"))
        {
            string seedType = other.gameObject.tag; // Pobierz typ nasiona z tagu
            if (seeds.ContainsKey(seedType))
            {
                seeds[seedType]++;
                PlayerPrefs.SetInt(seedType, seeds[seedType]);
                PlayerPrefs.Save(); // Wymuszamy zapis danych
                Destroy(other.gameObject); // Usuń nasiono z gry
                Debug.Log($"{seedType} zebrane! Ilość: {seeds[seedType]}");
            }
        }
    }

    public int GetSeedCount(string seedType)
    {
        return seeds.ContainsKey(seedType) ? seeds[seedType] : 0;
    }

    public void UseSeedToPlant(string seedType)
    {
        if (seeds.ContainsKey(seedType) && seeds[seedType] > 0)
        {
            seeds[seedType]--;
            PlayerPrefs.SetInt(seedType, seeds[seedType]);
            PlayerPrefs.Save(); // Wymuszamy zapis danych
        }
    }
}
