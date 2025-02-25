using UnityEngine;

public class FishEating : MonoBehaviour
{
    public float growthFactor = 0.1f; // How much the fish grows per eaten animal
    public string animalTag = "Animal"; // Tag for spawned animals

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            EatNearestAnimal();
        }
    }

    void EatNearestAnimal()
    {
        Collider2D[] nearbyAnimals = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        Collider2D nearestAnimal = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D animal in nearbyAnimals)
        {
            if (animal.CompareTag(animalTag))
            {
                float distance = Vector2.Distance(transform.position, animal.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestAnimal = animal;
                }
            }
        }

        if (nearestAnimal != null)
        {
            Destroy(nearestAnimal.gameObject);
            GrowFish();
        }
    }

    void GrowFish()
    {
        transform.localScale += new Vector3(growthFactor, growthFactor, 0);
    }
}