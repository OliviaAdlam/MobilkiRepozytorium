using UnityEngine;

public class FlShopTrig : MonoBehaviour
{
    public FlowerShopSystem flowerShopSystem;

    void Start()
    {
        if (flowerShopSystem == null)
        {
            Debug.LogError("FlowerShopSystem not assigned in the inspector!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            flowerShopSystem.OpenShop(); // Upewnij się, że ta metoda istnieje i jest dostępna
        }
    }
}
