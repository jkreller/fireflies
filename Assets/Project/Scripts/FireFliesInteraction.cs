using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class FireFliesInteraction : ControllerComponent
{
    // References
    [SerializeField] private SteamVR_Action_Boolean grabPinchAction;
    [SerializeField] private Material glowingMaterial;
    [SerializeField] private Controller otherController;
    private Fireflies fireflies;
    private Light lightObject;
    private MeshRenderer[] meshRenderers;
    private Material[] oldMaterials;

    // Logic fields
    [System.NonSerialized] public bool moveWithMouse;
    [System.NonSerialized] public bool shouldFollow;
    [SerializeField] private float minSeperationDragDistance = 0.5f;
    private bool isGlowing;
    private Vector3 seperatingStartPosition;

    private void Awake()
    {
        fireflies = GameObject.Find("Fireflies").GetComponent<Fireflies>();
        lightObject = GetComponentInChildren<Light>();
        if (lightObject)
        {
            lightObject.enabled = false;
        }
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
                seperatingStartPosition = transform.position;
            } else if (grabPinchAction.GetLastStateUp(handType))
            {
                // Set that fireflies should not follow
                shouldFollow = false;

                // Deactivate controller glowing
                if (isGlowing)
                {
                    Glow(false);
                }

                // Logic for seperating
                if (seperatingStartPosition != null)
                {
                    float dragDistance = Vector3.Distance(transform.position, seperatingStartPosition);
                    if (dragDistance > minSeperationDragDistance)
                    {
                        // Todo
                    }
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
}