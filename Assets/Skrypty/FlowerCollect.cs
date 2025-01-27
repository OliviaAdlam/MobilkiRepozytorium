using UnityEngine;

public class FlowerCollect : MonoBehaviour
{
    private int flowers = 0;

    void Start()
    {
        flowers = PlayerPrefs.GetInt("Flowers", 0); 
        Debug.Log("Liczba kwiatów: " + flowers);
    }

    // Funkcja wywoływana, gdy gracz wchodzi w kontakt z kwiatem
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Flower")) // Tylko kwiaty
        {
            flowers++;
            Debug.Log("Kwiaty: " + flowers);
            PlayerPrefs.SetInt("Flowers", flowers);
            Destroy(other.gameObject); // Niszczenie kwiatu po zebraniu
        }
    }

    // Funkcja do dodania kwiatów (jeśli potrzebujesz w jakimś miejscu dodać ręcznie)
    public void AddFlower()
    {
        flowers++;
        PlayerPrefs.SetInt("Flowers", flowers);
        Debug.Log("Liczba kwiatów: " + flowers);
    }

    // Funkcja do pobrania liczby kwiatów
    public int GetFlowerCount()
    {
        return flowers;
    }

    // Funkcja do usuwania określonej liczby kwiatów (np. przy sprzedaży)
    public void RemoveFlowers(int count)
    {
        flowers -= count;
        if (flowers < 0) flowers = 0;  // Zapobiega negatywnej liczbie
        PlayerPrefs.SetInt("Flowers", flowers);
        Debug.Log("Liczba kwiatów po sprzedaży: " + flowers);
    }
}
