using UnityEngine;

public class Pot : MonoBehaviour
{
    public GameObject seedPrefab;
    public GameObject flowerPrefab;
    public float growthTime = 300f;
    public Vector3 grownSize = new Vector3(1f, 1f, 1f);
    public float seedOffsetY = 0.5f;

    private GameObject currentSeed;
    private bool isSeedPlanted = false;
    private float growthTimer = 0f;
    private bool isPlayerNear = false;  // Dodano zmienną do wykrywania, czy gracz jest w pobliżu doniczki

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSeedPlanted && isPlayerNear)  // Dodajemy sprawdzenie, czy gracz jest blisko
        {
            PlantSeed();
        }

        if (isSeedPlanted && currentSeed != null)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;  // Gracz wchodzi w obszar doniczki
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;  // Gracz wychodzi z obszaru doniczki
        }
    }

    void PlantSeed()
    {
        SeedCollect seedCollect = FindObjectOfType<SeedCollect>();

        if (seedCollect != null)
        {
            string potTag = gameObject.tag;
            bool canPlant = false;

            // Sprawdzamy, czy potTag odpowiada SeedCollect
            switch (potTag)
            {
                case "Pot1":
                    if (isPlayerNear && seedCollect.GetSeedCount("Seed1") > 0)  // Sprawdzamy, czy gracz jest blisko i ma odpowiednie nasiono
                    {
                        seedCollect.UseSeedToPlant("Seed1");
                        canPlant = true;
                    }
                    break;
                case "Pot2":
                    if (isPlayerNear && seedCollect.GetSeedCount("Seed2") > 0)
                    {
                        seedCollect.UseSeedToPlant("Seed2");
                        canPlant = true;
                    }
                    break;
                case "Pot3":
                    if (isPlayerNear && seedCollect.GetSeedCount("Seed3") > 0)
                    {
                        seedCollect.UseSeedToPlant("Seed3");
                        canPlant = true;
                    }
                    break;
            }

            if (canPlant)
            {
                Vector3 plantPosition = transform.position + new Vector3(0, seedOffsetY, 0);
                currentSeed = Instantiate(seedPrefab, plantPosition, Quaternion.identity);
                isSeedPlanted = true;
                growthTimer = 0f;
            }
            else
            {
                Debug.Log($"Nie masz odpowiedniego nasiona dla doniczki {potTag} lub nie jesteś blisko!");
            }
        }
    }

    void GrowFlower()
    {
        if (currentSeed != null)
        {
            Destroy(currentSeed);
            Vector3 flowerPosition = transform.position + new Vector3(0, seedOffsetY, 0);
            Instantiate(flowerPrefab, flowerPosition, Quaternion.identity);
        }

        isSeedPlanted = false;
    }
}
