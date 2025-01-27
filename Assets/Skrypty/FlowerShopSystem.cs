using UnityEngine;
using UnityEngine.UI;

public class FlowerShopSystem : MonoBehaviour
{
    public GameObject flowersTab;
    public GameObject potsTab;
    public Button flowersTabButton;
    public Button potsTabButton;

    public Button sellFlowersButton;
    public Text flowersText;
    public Text bouquetsText;
    public int flowersToSell = 3;
    private int bouquets = 3;

    public Button buyPotButton1;
    public Button buyPotButton2;
    public Text potsText;
    private int pots = 0;

    public GameObject pot1;
    public GameObject pot2;

    private FlowerCollect flowerCollect;

    public GameObject shopPanel;

    void Start()
    {
        flowerCollect = FindObjectOfType<FlowerCollect>();

        flowersTab.SetActive(true);
        potsTab.SetActive(false);

        flowersTabButton.onClick.AddListener(() => SwitchTab(true));
        potsTabButton.onClick.AddListener(() => SwitchTab(false));
        sellFlowersButton.onClick.AddListener(SellFlowers);
        buyPotButton1.onClick.AddListener(() => BuyPot(1));
        buyPotButton2.onClick.AddListener(() => BuyPot(2));

        LoadPurchasedPots(); // Załaduj stan zakupionych doniczek
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }

    private void SwitchTab(bool toFlowersTab)
    {
        flowersTab.SetActive(toFlowersTab);
        potsTab.SetActive(!toFlowersTab);
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

    private void SellFlowers()
    {
        int flowerCount = flowerCollect.GetFlowerCount();

        if (flowerCount >= flowersToSell)
        {
            flowerCollect.RemoveFlowers(flowersToSell);
            bouquets++;
            UpdateUI();
            Debug.Log("Sprzedano 3 kwiaty za 1 bukiet.");
        }
        else
        {
            Debug.Log("Nie masz wystarczającej liczby kwiatów.");
        }
    }

    private void BuyPot(int potPrice)
    {
        if (bouquets >= potPrice)
        {
            bouquets -= potPrice;
            pots++;
            UpdateUI();

            if (potPrice == 1 && pot1 != null)
            {
                pot1.SetActive(true);
                buyPotButton1.interactable = false; // Dezaktywacja przycisku po zakupie
                PlayerPrefs.SetInt("Pot1Bought", 1); // Zapisanie stanu zakupu doniczki
                Debug.Log("Kupiono doniczkę 1 za 1 bukiet!");
            }
            else if (potPrice == 2 && pot2 != null)
            {
                pot2.SetActive(true);
                buyPotButton2.interactable = false; // Dezaktywacja przycisku po zakupie
                PlayerPrefs.SetInt("Pot2Bought", 1); // Zapisanie stanu zakupu doniczki
                Debug.Log("Kupiono doniczkę 2 za 2 bukiety!");
            }
        }
        else
        {
            Debug.Log("Za mało bukietów na zakup doniczki!");
        }
    }

    private void LoadPurchasedPots()
    {
        // Załaduj stan zakupu doniczek
        if (PlayerPrefs.GetInt("Pot1Bought", 0) == 1)
        {
            pot1.SetActive(true);
            buyPotButton1.interactable = false; // Dezaktywacja przycisku po zakupie
        }

        if (PlayerPrefs.GetInt("Pot2Bought", 0) == 1)
        {
            pot2.SetActive(true);
            buyPotButton2.interactable = false; // Dezaktywacja przycisku po zakupie
        }
    }

    private void UpdateUI()
    {
        flowersText.text = $"Kwiaty: {flowerCollect.GetFlowerCount()}";
        bouquetsText.text = $"Bukiety: {bouquets}";
        potsText.text = $"Doniczki: {pots}";
    }
}
