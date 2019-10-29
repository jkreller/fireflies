using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class FirefliesInteraction : ControllerComponent
{
    // References
    [SerializeField] private SteamVR_Action_Boolean grabPinchAction;
    [SerializeField] private Material glowingMaterial;
    [SerializeField] private Controller otherController;
    private Fireflies firefliesHitted;
    private Light lightObject;
    private MeshRenderer[] meshRenderers;
    private Material[] oldMaterials;

    // Logic fields
    [System.NonSerialized] public bool moveWithMouse;
    [System.NonSerialized] public bool shouldFollow;
    [SerializeField] private float minSeperationDragDistance = 0.05f;
    private bool isGlowing;
    private Vector3? dragStartPosition;

    private void Awake()
    {
        lightObject = GetComponentInChildren<Light>();
        if (lightObject)
        {
            lightObject.enabled = false;
        }

        print(name + ": " + GetInstanceID());
    }

    void Update()
    {
        if (XRDevice.isPresent)
        {
            if (grabPinchAction.GetStateDown(handType))
            {
                // Set that fireflies should follow
                shouldFollow = true;

                // Make controller glowing
                if (!isGlowing)
                {
                    Glow(true);
                }

                // Logic for seperating
                dragStartPosition = transform.position;
            } else if (grabPinchAction.GetLastStateUp(handType))
            {
                // Set that fireflies should not follow
                shouldFollow = false;

                // Deactivate controller glowing
                if (isGlowing)
                {
                    Glow(false);
                }

                // Reset values of seperation
                ResetDrag();
            }

            // Logic for seperating
            if (dragStartPosition != null && firefliesHitted != null)
            {
                float dragDistance = Vector3.Distance(transform.position, (Vector3) dragStartPosition);
                var allowed = FirefliesHelper.Instance.RequestSeperating(GetInstanceID(), firefliesHitted.GetInstanceID());
                if (dragDistance > minSeperationDragDistance && allowed)
                {
                    firefliesHitted.Seperate();
                }
            }
        } else if (moveWithMouse)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = Input.mousePosition.z + 2;
            transform.position = Camera.main.ScreenToWorldPoint(temp);
        } else
        {
            shouldFollow = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Fireflies fireflies = other.gameObject.GetComponent<Fireflies>();
        if (fireflies)
        {
            firefliesHitted = fireflies;
        }
    }

    // Change controller material and add light for attracting and seperating
    private void Glow(bool turnOn)
    {
        if (turnOn)
        {
            if (meshRenderers == null)
            {
                meshRenderers = GetComponentsInChildren<MeshRenderer>();
                oldMaterials = new Material[meshRenderers.Length];
                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    oldMaterials[i] = meshRenderers[i].material;
                }
            }

            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.material = glowingMaterial;
            }

            lightObject.enabled = true;
            isGlowing = true;
        } else
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = oldMaterials[i];
            }
            lightObject.enabled = false;
            isGlowing = false;
        }
    }

    public void ResetDrag()
    {
        dragStartPosition = null;
        if (firefliesHitted)
        {
            FirefliesHelper.Instance.ResetSeperationCount(firefliesHitted.GetInstanceID());
        }
    }
}