using UnityEngine;

public class Pot : MonoBehaviour
{
    public GameObject seedPrefab;
    public GameObject flowerPrefab;
    public float growthTime = 300f;  // Czas wzrostu w sekundach (5 minut)
    public Vector3 grownSize = new Vector3(1f, 1f, 1f); // Docelowy rozmiar kwiatu
    public float seedOffsetY = 0.5f;  // Odległość nasiona od doniczki w osi Y (nad doniczką)

    private GameObject currentSeed;
    private bool isSeedPlanted = false;
    private float growthTimer = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSeedPlanted)
        {
            PlantSeed();
        }

        if (isSeedPlanted && currentSeed != null) // Sprawdzamy, czy currentSeed nie jest null
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= growthTime)
            {
                GrowFlower();
            }
            else
            {
                float growthProgress = growthTimer / growthTime;
                currentSeed.transform.localScale = Vector3.Lerp(Vector3.one, grownSize, growthProgress);
            }
        }
    }

    void PlantSeed()
    {
        Vector3 plantPosition = transform.position + new Vector3(0, seedOffsetY, 0);  // Ustawienie nasiona nad doniczką
        currentSeed = Instantiate(seedPrefab, plantPosition, Quaternion.identity);
        isSeedPlanted = true;
        growthTimer = 0f;
    }

    void GrowFlower()
    {
        if (currentSeed != null)  // Upewniamy się, że currentSeed nie jest null
        {
            Destroy(currentSeed);
            Instantiate(flowerPrefab, transform.position + new Vector3(0, seedOffsetY, 0), Quaternion.identity);
        }

        isSeedPlanted = false;
    }
}
