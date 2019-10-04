using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for fireflies particle system
public class Fireflies : MonoBehaviour
{
    // Options
    [SerializeField] private float seperationDistance = 2.5f;
    [SerializeField] private float minScale = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        // If is colliding with another fireflies object
        if (other.gameObject.CompareTag("Fireflies"))
        {
            if (FirefliesMerger.Instance.RequestMerging())
            {
                Merge(other.gameObject);
            }
        }
    }

    // Seperate one fireflies object into two objects of half size
    public void Seperate(Vector3 playerPosition)
    {
        if ((transform.localScale * 0.5f).sqrMagnitude >= (new Vector3(0.5f, 0.5f, 0.5f)).sqrMagnitude)
        {
            Vector3 normalizedDirection = (playerPosition - transform.position).normalized;

            Vector3 directionParts = Quaternion.AngleAxis(90, Vector3.up) * normalizedDirection * seperationDistance;

            Vector3 posPart1 = transform.position + directionParts;
            Vector3 posPart2 = transform.position - directionParts;

            GameObject partObject = gameObject;
            partObject.transform.localScale *= 0.5f;

            Instantiate(partObject, posPart1, transform.rotation).GetComponent<Fireflies>();
            Instantiate(partObject, posPart2, transform.rotation).GetComponent<Fireflies>();

            Destroy(gameObject);
        }
    }

    // Merge fireflies together with another one into one fireflies object with double size
    private void Merge(GameObject other)
    {
        GameObject mergedObject = gameObject;
        mergedObject.transform.position = transform.position + (other.transform.position - transform.position) * 0.5f;
        mergedObject.transform.localScale = transform.localScale + other.transform.localScale;

        Instantiate(mergedObject);

        Destroy(gameObject);
        Destroy(other);
    }
}
