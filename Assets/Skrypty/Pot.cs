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
    SeedCollect seedCollect = FindObjectOfType<SeedCollect>();

    if (seedCollect != null && seedCollect.GetSeedCount() > 0)
    {
        seedCollect.UseSeedToPlant();
        Vector3 plantPosition = transform.position + new Vector3(0, seedOffsetY, 0);
        currentSeed = Instantiate(seedPrefab, plantPosition, Quaternion.identity);
        isSeedPlanted = true;
        growthTimer = 0f;
    }

    }


    void GrowFlower()
    {
    if (currentSeed != null)
    {
        Destroy(currentSeed);
        Vector3 flowerPosition = transform.position + new Vector3(0, seedOffsetY, 0); // Ustawienie pozycji kwiatu
        Instantiate(flowerPrefab, flowerPosition, Quaternion.identity);
    }

    isSeedPlanted = false;
    }
}
