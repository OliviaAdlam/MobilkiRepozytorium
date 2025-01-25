using UnityEngine;

public class FlShopTrig : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<FlowerShopSystem>().OpenShop();
        }
    }
}
