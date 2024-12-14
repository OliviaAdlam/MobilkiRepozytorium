using UnityEngine;

public class FlowerCollect : MonoBehaviour
{
    private int flowers = 0;

    void Start()
    {
        flowers = PlayerPrefs.GetInt("Flowers", 0); 
        Debug.Log("Liczba kwiatów: " + flowers);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Flower"))
        {
            flowers++;
            Debug.Log("Kwiaty: " + flowers);
            PlayerPrefs.SetInt("Flowers", flowers);
            Destroy(other.gameObject);
        }
    }

    public void AddFlower()
    {
        flowers++;
        PlayerPrefs.SetInt("Flowers", flowers);
        Debug.Log("Liczba kwiatów: " + flowers);
    }

    public int GetFlowerCount()
    {
        return flowers;
    }

    public void RemoveFlowers(int count)
    {
        flowers -= count;
        if (flowers < 0) flowers = 0;  // Zapobiega negatywnej liczbie
        PlayerPrefs.SetInt("Flowers", flowers);
        Debug.Log("Liczba kwiatów po sprzedaży: " + flowers);
    }
}
