using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for fireflies particle system
public class Fireflies : Movable
{
    
    // Options
    [Header("Fireflies Options")]
    [SerializeField] private bool initializeToLamp = true;
    public int firefliesSize;

    // References
    private Animator animator;

    // Logic fields
    private float seperationDistance;
    private static float minScale; // one fourth of initial size, for example 0.5 if initial scale is 2
    private bool isInitialFireflies;
    public bool deactivateFirefly;
    public bool flyToNextPoint = false;
    private List<GameObject> pathMakingObejcts;

    //Fireflies follow
    FireFliesAttract fireFliesAttract;

    new void Update()
    {
        base.Update();
        //Debug.Log(this.gameObject.transform.localScale);


        if (this.gameObject.transform.localScale.x < 0.6 && this.gameObject.transform.localScale.x > 0.16)
        {
            firefliesSize = 2;
        }
        else if(this.gameObject.transform.localScale.x > 0.4)
        {
            firefliesSize = 1;
        }
        else if (this.gameObject.transform.localScale.x < 0.2)
        {
            firefliesSize = 4;
        }
        if(deactivateFirefly){
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }
        }

    }

    void Start()
    {
        pathMakingObejcts = new List<GameObject>();

        float avrgScale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;

        animator = GetComponent<Animator>();
        SetAnimState(3);

        seperationDistance = avrgScale;

        // If is inital fireflies object
        if (transform.name == "Fireflies")
        {
            isInitialFireflies = true;
            minScale = avrgScale * 0.25f;
        }

        // Initialize to lamp
        if (initializeToLamp && isInitialFireflies)
        {
            GameObject lamp = GameObject.Find("Lamp");
            transform.position = lamp.transform.position + Vector3.up * 0.1f;
            SetAnimState(1);
        }

        //get attractBall
        fireFliesAttract = GameObject.Find("AttractBall").GetComponent<FireFliesAttract>();
    }



    void OnEnable()
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
                if (FirefliesMerger.Instance.RequestMerging())
                {
                    Merge(other.gameObject);
                }
            }
            // go into cage1
            if (other.gameObject.CompareTag("cage1"))
                {
                if (firefliesSize == 2)
                {
                    pathMakingObejcts.Add(GameObject.Find("cage1point1"));
                    pathMakingObejcts.Add(GameObject.Find("cage1point2"));
                    pathMakingObejcts.Add(GameObject.Find("cage1point3"));
                    pathFinding(pathMakingObejcts);
                    cageDoor cageDoor1 = GameObject.Find("CageDoor1").GetComponent<cageDoor>();
                    cageDoor1.closeCage = true;
                }
               }
            // go into cage2
            if (other.gameObject.CompareTag("cage2"))
            {
                if (firefliesSize == 2)
                {
                    pathMakingObejcts.Add(GameObject.Find("cage2point1"));
                    pathMakingObejcts.Add(GameObject.Find("cage2point2"));
                    pathMakingObejcts.Add(GameObject.Find("cage2point3"));
                    pathFinding(pathMakingObejcts);
                    cageDoor cageDoor2 = GameObject.Find("CageDoor2").GetComponent<cageDoor>();
                    cageDoor2.closeCage = true;
                }
            }
        }
    }
    private void pathFinding(List<GameObject> pathBubbles){
        
        foreach(GameObject bubble in pathBubbles){
            StartMovement(bubble.transform.position);
            StartCoroutine(WaitforPoint());
            flyToNextPoint = false;
            Destroy(bubble);
        }
        deactivateFirefly = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!deactivateFirefly)
        {
            // Follow fire flies attract object
            FireFliesAttract fireFliesAttract = other.gameObject.GetComponent<FireFliesAttract>();
            if (fireFliesAttract && fireFliesAttract.shouldFollow)
            {
                StartMovement(other.gameObject.transform.position);
            }
        }
    }

    IEnumerator WaitforPoint()
    {
        yield return new WaitUntil(() => flyToNextPoint == true);
    }

    // Seperate one fireflies object into two objects of half size
    public void Seperate(Vector3 playerPosition)
    {
        if (!deactivateFirefly)
        {
            // Check if is big enough to seperate
            if ((transform.localScale * 0.5f).sqrMagnitude >= (new Vector3(minScale, minScale, minScale)).sqrMagnitude)
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
    }

    public void Activate(Vector3 playerPosition)
    {
        SetAnimState(2);
        StartMovement(playerPosition + Vector3.back);
    }

    // Merge fireflies together with another one into one fireflies object with double size
    private void Merge(GameObject other)
    {
        if (!deactivateFirefly)
        {
            GameObject mergedObject = gameObject;
            mergedObject.transform.position = transform.position + (other.transform.position - transform.position) * 0.5f;
            mergedObject.transform.localScale = transform.localScale + other.transform.localScale;

            Instantiate(mergedObject);

            Destroy(gameObject);
            Destroy(other);
        }
    }

    private void SetAnimState(int state)
    {
        animator.SetInteger("AnimState", state);
    }
}
