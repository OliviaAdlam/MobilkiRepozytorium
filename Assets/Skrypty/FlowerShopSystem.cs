using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FlowerShopSystem : MonoBehaviour
{
    [Header("Bukiety i zakupy")]
    public Text bouquetsText; // Tekst wyświetlający liczbę bukietów
    public Text selectedFlowersText; // Tekst wyświetlający zaznaczone kwiaty
    public Button convertButton; // Przycisk konwersji na bukiety

    [Header("Zakładki sklepu")]
    public GameObject bouquetsTab; // Zakładka bukiety
    public GameObject potsTab; // Zakładka doniczki
    public Button bouquetsTabButton; // Przycisk przełączania do zakładki bukiety
    public Button potsTabButton; // Przycisk przełączania do zakładki doniczki

    [Header("Doniczki")]
    public Text potsText; // Tekst wyświetlający liczbę doniczek
    public Button buyPotButton; // Przycisk zakupu doniczek
    public int potPrice = 5; // Cena doniczki

    [Header("Kwiaty i ikony")]
    public List<Image> flowerIcons; // Ikony kwiatów w UI

    // Panel sklepu
    public GameObject shopPanel; // Cały panel sklepu, który zawiera zakładki i inne elementy UI

    private Dictionary<string, int> selectedFlowers = new Dictionary<string, int>
    {
        { "Flower1", 0 },
        { "Flower2", 0 },
        { "Flower3", 0 }
    };

    private int bouquets = 0;
    private int pots = 0;

    void Start()
    {
        bouquetsTab.SetActive(true);
        potsTab.SetActive(false);
        shopPanel.SetActive(false); // Na początku sklep jest zamknięty

        bouquetsTabButton.onClick.AddListener(() => SwitchTab(true));
        potsTabButton.onClick.AddListener(() => SwitchTab(false));
        convertButton.onClick.AddListener(GenerateBouquets);
        buyPotButton.onClick.AddListener(BuyPot);

        UpdateUI();
    }

    void Update()
    {
        // Sprawdzenie, czy klawisz ESC został naciśnięty i sklep jest otwarty
        if (Input.GetKeyDown(KeyCode.Escape) && shopPanel.activeSelf)
        {
            CloseShop(); // Zamknięcie sklepu
        }
    }

    // Funkcja do otwierania sklepu
    public void OpenShop()
    {
        shopPanel.SetActive(true); // Aktywujemy panel sklepu
        UpdateUI(); // Odświeżamy UI, żeby pokazać aktualny stan
    }

    // Funkcja do zamykania sklepu
    public void CloseShop()
    {
        shopPanel.SetActive(false); // Dezaktywujemy panel sklepu
    }

    // Funkcja do klikania kwiatów
    public void OnFlowerClick(string flowerTag)
    {
        if (selectedFlowers.ContainsKey(flowerTag))
        {
            selectedFlowers[flowerTag]++; // Zwiększamy liczbę wybranego kwiatu
            HighlightFlower(flowerTag); // Podświetlamy kliknięty kwiat
            UpdateUI(); // Odświeżamy UI
        }
        else
        {
            Debug.LogWarning($"Nieznany tag kwiatu: {flowerTag}");
        }
    }

    private void HighlightFlower(string flowerTag)
    {
        foreach (Image icon in flowerIcons)
        {
            if (icon.CompareTag(flowerTag))
            {
                icon.color = Color.yellow; // Podświetlenie klikniętego kwiatu
            }
        }
    }

    // Funkcja do generowania bukietów
    private void GenerateBouquets()
    {
        int flower1Count = selectedFlowers["Flower1"];
        int flower2Count = selectedFlowers["Flower2"];
        int flower3Count = selectedFlowers["Flower3"];

        int newBouquets = 0;

        // Tworzenie bukietów
        while (flower1Count >= 3)
        {
            flower1Count -= 3;
            newBouquets++;
        }
        while (flower1Count >= 1 && flower2Count >= 1)
        {
            flower1Count -= 1;
            flower2Count -= 1;
            newBouquets++;
        }
        while (flower1Count >= 2 && flower3Count >= 1)
        {
            flower1Count -= 2;
            flower3Count -= 1;
            newBouquets += 2; // 2 bukiety
        }

        bouquets += newBouquets;

        // Aktualizacja zaznaczonych kwiatów
        selectedFlowers["Flower1"] = flower1Count;
        selectedFlowers["Flower2"] = flower2Count;
        selectedFlowers["Flower3"] = flower3Count;

        UpdateUI();
    }

    // Funkcja do zakupu doniczki
    private void BuyPot()
    {
        if (bouquets >= potPrice)
        {
            bouquets -= potPrice;
            pots++;
            UpdateUI();
            Debug.Log("Kupiono doniczkę!");
        }
        else
        {
            Debug.Log("Za mało bukietów na zakup doniczki!");
        }
    }

    private void SwitchTab(bool toBouquetsTab)
    {
        bouquetsTab.SetActive(toBouquetsTab);
        potsTab.SetActive(!toBouquetsTab);
    }

    private void UpdateUI()
    {
        bouquetsText.text = $"Bukiety: {bouquets}";
        selectedFlowersText.text = $"Zaznaczone: {selectedFlowers["Flower1"]}x Flower1, {selectedFlowers["Flower2"]}x Flower2, {selectedFlowers["Flower3"]}x Flower3";
        potsText.text = $"Doniczki: {pots}";

        ResetFlowerIcons();
    }

    private void ResetFlowerIcons()
    {
        foreach (Image icon in flowerIcons)
        {
            icon.color = Color.white; // Resetowanie podświetlenia
        }

        foreach (var flowerTag in selectedFlowers.Keys)
        {
            if (selectedFlowers[flowerTag] > 0)
            {
                foreach (Image icon in flowerIcons)
                {
                    if (icon.CompareTag(flowerTag))
                    {
                        icon.color = Color.yellow; // Oznacz zaznaczone
                    }
                }
            }
        }
    }
}