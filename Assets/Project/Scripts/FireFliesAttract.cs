using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class FireFliesAttract : ControllerComponent
{
    // References
    private Fireflies fireflies;
    [SerializeField] private SteamVR_Action_Boolean grabPinchAction;
    [SerializeField] private Material glowingMaterial;
    private Light lightObject;
    private MeshRenderer[] meshRenderers;
    private Material[] oldMaterials;

    // Logic fields
    [System.NonSerialized] public bool moveWithMouse;
    [System.NonSerialized] public bool shouldFollow;
    private bool isGlowing;

    private void Awake()
    {
        fireflies = GameObject.Find("Fireflies").GetComponent<Fireflies>();
        lightObject = GetComponentInChildren<Light>();
        lightObject.enabled = false;
    }

    void Update()
    {
        if (XRDevice.isPresent)
        {
            if (grabPinchAction.GetStateDown(handType))
            {
                shouldFollow = true;
                if (!isGlowing)
                {
                    Glow(true);
                }
            } else if (grabPinchAction.GetLastStateUp(handType))
            {
                shouldFollow = false;
                if (isGlowing)
                {
                    Glow(false);
                }
            }
        } else if (moveWithMouse)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = Input.mousePosition.z + 1.5f;
            this.transform.position = Camera.main.ScreenToWorldPoint(temp);
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