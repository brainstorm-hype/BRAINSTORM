using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Hand_Own : MonoBehaviour
{

    public SteamVR_Action_Boolean grabAction = null;

    private SteamVR_Behaviour_Pose pose = null;

    private FixedJoint joint = null;

    private Interactable currentInteractable = null;

    public List<Interactable> contactInteractables = new List<Interactable>();


    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();

    }

    void Update()
    {
        // Down
        if (grabAction.GetStateDown(pose.inputSource))
        {
            print(pose.inputSource + "Trigger Down");
            Pickup();
        }
        // Up
        if (grabAction.GetLastStateUp(pose.inputSource))
        {
            print(pose.inputSource + "Trigger Up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }
        contactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }
        contactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {

    }

    public void Drop()
    {

    }

    private Interactable GetNearestInteractable()
    {
        return null;
    }
}
