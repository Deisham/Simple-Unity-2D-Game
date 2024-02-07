using System.Collections;
using UnityEngine;

public class ArrowTrapLauncherLeft : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform launchPoint;
    public float launchInterval = 2f;

    private void Start()
    {
        StartCoroutine(LaunchArrows());
    }

    IEnumerator LaunchArrows()
    {
        while (true)
        {
            yield return new WaitForSeconds(launchInterval);

            if (arrowPrefab != null && launchPoint != null)
            {
                GameObject arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity);
                ArrowProjectileLeft arrowProjectile = arrow.GetComponent<ArrowProjectileLeft>();
                if (arrowProjectile != null)
                {
                    arrowProjectile.Launch(Vector2.right); 
                }
            }
        }
    }
}
