using Cainos.PixelArtPlatformer_VillageProps;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // Клавиша для взаимодействия, установите нужную
    public float interactionRange = 3f; // Расстояние для взаимодействия, установите нужное

    private List<Chest> chests = new List<Chest>();
    public HealthSystem playerHealth;

    void Start()
    {
        Chest[] allChests = FindObjectsOfType<Chest>();
        chests.AddRange(allChests);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Chest closestChest = FindClosestChest();

            if (closestChest != null && !closestChest.IsOpened) // Добавлено условие проверки открытости сундука
            {
                float distanceToChest = Vector3.Distance(transform.position, closestChest.transform.position);

                if (distanceToChest <= interactionRange)
                {
                    closestChest.Open();
                    playerHealth.AddHealth(10);
                }
            }
        }
    }

    private Chest FindClosestChest()
    {
        Chest closestChest = null;
        float closestDistance = float.MaxValue;

        foreach (Chest chest in chests)
        {
            float distance = Vector3.Distance(transform.position, chest.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestChest = chest;
            }
        }

        return closestChest;
    }
}
