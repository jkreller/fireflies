using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for fireflies particle system
public class Fireflies : Movable
{
    // Options
    [Header("Fireflies Options")]
    [SerializeField] private bool initializeToLamp = true;
    [SerializeField] private float seperationDistanceFactor = 0.8f;

    // References
    private Animator animator;
    private GameObject player;

    // Logic fields
    [System.NonSerialized] public bool flyToNextPoint = false;
    [System.NonSerialized] public int firefliesCount;
    private float seperationDistance;
    private static float minScale; // one fourth of initial size, for example 0.5 if initial scale is 2
    private bool isInitialFireflies;
    private bool deactivateFirefly;
    private List<GameObject> pathMakingObejcts = new List<GameObject>();
    private float avrgScale;
    private FirefliesInteraction fireFliesAttract;
    private List<FirefliesInteraction> firefliesInteractions = new List<FirefliesInteraction>();

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject controller in GameObject.FindGameObjectsWithTag("Controller"))
        {
            firefliesInteractions.Add(controller.GetComponent<FirefliesInteraction>());
        }

        SetAnimState(3);

        avrgScale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;

        // If is inital fireflies object
        if (transform.name == "Fireflies")
        {
            isInitialFireflies = true;
            minScale = avrgScale * 0.25f;
        }

        // Set how many fireflies should be present (fireflies count)
        if (avrgScale == minScale * 4)
        {
            firefliesCount = 1;
        }
        else if (avrgScale == minScale * 2)
        {
            firefliesCount = 2;
        }
        else if (avrgScale == minScale)
        {
            firefliesCount = 4;
        }

        // Initialize to lamp
        if (initializeToLamp && isInitialFireflies)
        {
            GameObject lamp = GameObject.Find("Lamp");
            transform.position = lamp.transform.position + Vector3.up * 0.1f;
            SetAnimState(1);
        }
    }

    private new void Update()
    {
        base.Update();

        if (deactivateFirefly)
        {
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        // On first enable - logic for diabeling and enabeling because of elevator
        if (initializeToLamp && isInitialFireflies)
        {
            SetAnimState(1);
            initializeToLamp = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!deactivateFirefly)
        {
            // If is colliding with another fireflies object
            if (other.gameObject.CompareTag("Fireflies"))
            {
                if (FirefliesHelper.Instance.RequestMerging())
                {
                    Merge(other.gameObject);
                }
            }
            // Go into cage1
            if (other.gameObject.CompareTag("Cage1"))
                {
                if (firefliesCount == 2)
                {
                    pathMakingObejcts.Add(GameObject.Find("cage1point1"));
                    pathMakingObejcts.Add(GameObject.Find("cage1point2"));
                    pathMakingObejcts.Add(GameObject.Find("cage1point3"));
                    PathFinding(pathMakingObejcts);
                    CageDoor cageDoor1 = GameObject.Find("CageDoor1").GetComponent<CageDoor>();
                    cageDoor1.closeCage = true;
                }
               }
            // Go into cage2
            if (other.gameObject.CompareTag("Cage2"))
            {
                if (firefliesCount == 2)
                {
                    pathMakingObejcts.Add(GameObject.Find("cage2point1"));
                    pathMakingObejcts.Add(GameObject.Find("cage2point2"));
                    pathMakingObejcts.Add(GameObject.Find("cage2point3"));
                    PathFinding(pathMakingObejcts);
                    CageDoor cageDoor2 = GameObject.Find("CageDoor2").GetComponent<CageDoor>();
                    cageDoor2.closeCage = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!deactivateFirefly)
        {
            // Follow fire flies attract object
            FirefliesInteraction fireFliesAttract = other.gameObject.GetComponent<FirefliesInteraction>();
            if (fireFliesAttract && fireFliesAttract.shouldFollow)
            {
                StartMovement(other.gameObject.transform.position);
            }
        }
    }

    private void SetAnimState(int state)
    {
        animator.SetInteger("AnimState", state);
    }

    // Merge fireflies together with another one into one fireflies object with double size
    private void Merge(GameObject other)
    {
        if (!deactivateFirefly)
        {
            // Prepare merged fireflies object
            GameObject mergedObject = gameObject;
            mergedObject.transform.position = transform.position + (other.transform.position - transform.position) * 0.5f;
            mergedObject.transform.localScale = transform.localScale + other.transform.localScale;

            // Reset drag of controllers because otherwise would instantly seperate fireflies again
            foreach (FirefliesInteraction firefliesInteraction in firefliesInteractions)
            {
                firefliesInteraction.ResetDrag();
            }

            // Instantiate new fireflies object
            Instantiate(mergedObject);

            // Destroy old fireflies object
            Destroy(gameObject);
            Destroy(other);
        }
    }

    private void PathFinding(List<GameObject> pathBubbles)
    {
        foreach (GameObject bubble in pathBubbles)
        {
            StartMovement(bubble.transform.position);
            StartCoroutine(WaitForPoint());
            flyToNextPoint = false;
            Destroy(bubble);
        }
        deactivateFirefly = true;
    }

    private IEnumerator WaitForPoint()
    {
        yield return new WaitUntil(() => flyToNextPoint == true);
    }

    // Move fireflies from lamp to player and size them to normal
    public void ActivateFromLamp(Vector3 playerPosition)
    {
        SetAnimState(2);
        StartMovement(playerPosition + Vector3.back);
    }

    // Seperate one fireflies object into two objects of half size
    public void Seperate()
    {
        if (!deactivateFirefly)
        {
            // If is big enough to seperate
            if (firefliesCount < 4)
            {
                // Get player position with height of fireflies and calculate direction vector between player and fireflies with length 1
                Vector3 fixedPlayerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                Vector3 normalizedDirection = (fixedPlayerPos - transform.position).normalized;

                // Turn direction vector by 90 degrees around y-axis and scale by fireflies size
                Vector3 directionParts = Quaternion.AngleAxis(90, Vector3.up) * normalizedDirection * avrgScale * seperationDistanceFactor;

                // Calculate points for new fireflies objects
                Vector3 posPart1 = transform.position + directionParts;
                Vector3 posPart2 = transform.position - directionParts;

                // Prepare new fireflies objects
                GameObject partObject = gameObject;
                partObject.transform.localScale *= 0.5f;

                // Instantiate new fireflies objects
                Instantiate(partObject, posPart1, transform.rotation);
                Instantiate(partObject, posPart2, transform.rotation);

                // Destroy old fireflies object
                Destroy(gameObject);
            }
        }
    }
}