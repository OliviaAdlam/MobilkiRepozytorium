using UnityEngine;
using UnityEngine.UI;

public class FlowerShop : MonoBehaviour
{
    public GameObject shopPanel; 
    public Text flowerCountText; 
    public GameObject flowerIconPrefab; 
    public Transform dropZone; 
    public Button sellButton; 
    public Text coinText; 

    private int flowerCount = 0;
    private int coins = 0;
    private int flowersInDropZone = 0;

    void Start()
    {
        shopPanel.SetActive(false); 
        flowerCount = PlayerPrefs.GetInt("Flowers", 0);
        coins = PlayerPrefs.GetInt("Coins", 0);
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shopPanel.activeSelf)
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        UpdateUI();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OnFlowerDropped()
    {
        if (flowerCount > 0)
        {
            flowersInDropZone++;
            flowerCount--;
            UpdateUI();
        }
    }

    public void SellFlowers()
    {
        if (flowersInDropZone >= 3)
        {
            coins += flowersInDropZone;
            flowersInDropZone = 0;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt("Flowers", flowerCount);
            UpdateUI();
        }
        else
        {
            Debug.Log("Za mało kwiatów do sprzedaży!");
        }
    }

    private void UpdateUI()
    {
        flowerCountText.text = $"Kwiaty: {flowerCount}";
        coinText.text = $"Monety: {coins}";
    }
}
