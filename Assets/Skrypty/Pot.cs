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

    private bool isPlayerNearby = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSeedPlanted && isPlayerNearby)
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

    void PlantSeed()
    {
        SeedCollect seedCollect = FindObjectOfType<SeedCollect>();

        if (seedCollect != null)
        {
            string potTag = gameObject.tag;
            bool canPlant = false;

            switch (potTag)
            {
                case "Pot1":
                    if (seedCollect.GetSeedCount("Seed1") > 0)
                    {
                        seedCollect.UseSeedToPlant("Seed1");
                        canPlant = true;
                    }
                    break;
                case "Pot2":
                    if (seedCollect.GetSeedCount("Seed2") > 0)
                    {
                        seedCollect.UseSeedToPlant("Seed2");
                        canPlant = true;
                    }
                    break;
                case "Pot3":
                    if (seedCollect.GetSeedCount("Seed3") > 0)
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
                Debug.Log($"Nie masz odpowiedniego nasiona dla doniczki {potTag}!");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
