using UnityEngine;

public class SeedCollect : MonoBehaviour
{
    private int seeds = 0;

    void Start()
    {
        seeds = PlayerPrefs.GetInt("Seeds", 0); 
        Debug.Log("Liczba nasion: " + seeds);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Seed"))
        {
            seeds++;
            Debug.Log("Nasiona: " + seeds);
            PlayerPrefs.SetInt("Seeds", seeds); 
            Destroy(other.gameObject);
        }
    }

    public void UseSeedToPlant()
    {
        if (seeds > 0)
        {
            seeds--;
            PlayerPrefs.SetInt("Seeds", seeds);
            Debug.Log("Pozostałe nasiona: " + seeds);
        }
        else
        {
            Debug.Log("Brak nasion do zasadzenia.");
        }
    }

    public int GetSeedCount()
    {
        return seeds;
    }
}
