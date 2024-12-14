using UnityEngine;

public class SeedCollect : MonoBehaviour
{
    private int seeds = 0;

    void Start()
    {
        seeds = PlayerPrefs.GetInt("Seeds", 0); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Seed"))
        {
            Seed seed = other.gameObject.GetComponent<Seed>();
            if (seed != null)
            {
                seeds++;
                Debug.Log("Nasiona: " + seeds);
                PlayerPrefs.SetInt("Seeds", seeds);
                Destroy(other.gameObject);
            }
        }
    }
}
