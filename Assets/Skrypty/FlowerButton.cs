using UnityEngine;
using UnityEngine.UI;

public class FlowerButton : MonoBehaviour
{
    public GameObject flowerPrefab;
    public Transform dropZone;  

    private FlowerCollect flowerCollect; 

    void Start()
    {
        flowerCollect = FindObjectOfType<FlowerCollect>();
        GetComponent<Button>().onClick.AddListener(OnFlowerButtonClick);
    }

    // Funkcja wywoływana po kliknięciu przycisku
    void OnFlowerButtonClick()
    {
        // Sprawdzamy, czy gracz ma kwiaty w ekwipunku
        if (flowerCollect.GetFlowerCount() > 0)
        {
            // Dodajemy kopię kwiatu do DropZone
            GameObject flower = Instantiate(flowerPrefab, dropZone.position, Quaternion.identity);
            flower.transform.SetParent(dropZone);  // Przypisanie kwiatu do DropZone, jeśli chcesz, żeby był w hierarchii

            // Usuwamy jeden kwiat z ekwipunku
            flowerCollect.RemoveFlowers(1);

            Debug.Log("Kwiat przeniesiony do DropZone.");
        }
        else
        {
            Debug.Log("Brak kwiatów do przeniesienia!");
        }
    }
}
