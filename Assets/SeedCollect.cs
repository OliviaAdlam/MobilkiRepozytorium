using UnityEngine;

public class SeedCollect : MonoBehaviour
{
    private int seeds = 0; // potem na E będzie można zobaczyć ekwipunek z nasionami

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Seed"))
        {
            Seed seed = other.gameObject.GetComponent<Seed>();
            if (seed != null)
            {
                seeds += 1;
                Debug.Log("Nasiona: " + seeds);
                Destroy(other.gameObject);
            }
        }
    }
}
