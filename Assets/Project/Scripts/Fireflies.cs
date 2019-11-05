using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Script for fireflies particle system
public class Fireflies : Movable
{
    // Options
    [Header("Fireflies Options")]
    [SerializeField] private float minScale; // one fourth of initial size, for example 0.5 if initial scale is 2
    [SerializeField] private Transform initializeTo;
    [SerializeField] private Transform afterInitializeTarget;
    [SerializeField] private float seperationDistanceFactor = 0.8f;
    [SerializeField] private Vector3 initOffset;


    // References
    private Animator animator;
    private GameObject player;

    // Logic fields
    [System.NonSerialized] public bool flyToNextPoint = false;
    [System.NonSerialized] public int firefliesCount;
    private float seperationDistance;
    private bool isInitialFireflies;
    public bool deactivateFirefly { get; set; }
    private List<GameObject> pathMakingObejcts = new List<GameObject>();
    private SphereCollider deactivateCollider;
    private int currentBubbleIndex;
    private Transform[] bubbles;
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

        if (minScale == 0)
        {
            minScale = avrgScale / 4;
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

        // Initialize to object
        if (initializeTo)
        {
            transform.position = initializeTo.position + initOffset;
            SetAnimState(1);
            deactivateFirefly = true;
            initializeTo = null;
        }

        // Add collider for deactivated fireflies
        deactivateCollider = gameObject.AddComponent<SphereCollider>();
        deactivateCollider.radius = 0.1f;
        deactivateCollider.isTrigger = true;
        deactivateCollider.enabled = false;
    }

    private new void Update()
    {
        base.Update();

        DeactivateColliders();

        if (bubbles != null)
        {
            if (currentBubbleIndex < bubbles.Length)
            {
                StartMovement(bubbles[currentBubbleIndex].position);
            }
            else
            {
                bubbles = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstBubble"))
        {
            FirstBubble firstBubble = other.gameObject.GetComponent<FirstBubble>();
          
            if (firstBubble.size == firefliesCount)
            { 
                deactivateFirefly = true;
                bubbles = other.GetComponentsInChildren<Transform>();
                currentBubbleIndex = 1;
            }
        }

        if (bubbles != null && currentBubbleIndex < bubbles.Length && other.gameObject.name == bubbles[currentBubbleIndex].name)
        {
            currentBubbleIndex++;
        }

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

            // go into cage1
            /*if (other.gameObject.CompareTag("cage1"))
                {
                if (firefliesCount == 4)
                {
                cageDoor cageDoor1 = GameObject.Find("CageDoor1").GetComponent<cageDoor>();
                    cageDoor1.closeCage = true;

                }
               }
            // Go into cage2
            if (other.gameObject.CompareTag("Cage2"))
            {
                if (firefliesCount == 4)
                {
                    pathMakingObejcts.Add(GameObject.Find("cage2point1"));
                    pathMakingObejcts.Add(GameObject.Find("cage2point2"));
                    pathMakingObejcts.Add(GameObject.Find("cage2point3"));
                    PathFinding(pathMakingObejcts);
                    CageDoor cageDoor2 = GameObject.Find("CageDoor2").GetComponent<CageDoor>();
                    cageDoor2.closeCage = true;
                    deactivateFirefly = true;
                }
            }

            if (other.gameObject.CompareTag("labyrinth"))
            {
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble2"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble3"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble4"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble5"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble6"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble7"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble8"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble9"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble10"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble11"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble12"));
                pathMakingObejcts.Add(GameObject.Find("labyrinthbubble13"));
               
                pathFinding(pathMakingObejcts);
            }*/
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

    public void SetAnimState(int state)
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

    private void DeactivateColliders()
    {
        if (deactivateFirefly && !deactivateCollider.enabled)
        {
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }
            deactivateCollider.enabled = true;
        }
        else if (!deactivateFirefly && deactivateCollider.enabled)
        {
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = true;
            }
            deactivateCollider.enabled = false;
        }
    }

    // Move fireflies from lamp to player and size them to normal
    public void ActivateAfterInitialize()
    {
        SetAnimState(2);
        if (afterInitializeTarget)
        {
            float oldMovementSpeed = movementSpeed;
            movementSpeed = 1;
            StartMovement(afterInitializeTarget.position, () => {
                movementSpeed = oldMovementSpeed;
                deactivateFirefly = false;
                afterInitializeTarget = null;
            });
        }
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