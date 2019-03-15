using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using static Valve.VR.InteractionSystem.Interactable;
public class Hand : MonoBehaviour
{

    public SteamVR_Action_Boolean triggerAction = null;
    public SteamVR_Action_Boolean touchPadAction;
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
        if (triggerAction.GetStateDown(pose.inputSource))
        {
            print(pose.inputSource + "Trigger Down");

            if (currentInteractable != null)
            {
                currentInteractable.Action();
                return;
            }
            Pickup();
        }
        // Touchpad drop
        if (touchPadAction.GetStateDown(pose.inputSource))
        {
            print(pose.inputSource + "Touchpad Up");

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

        //Get nearest
        currentInteractable = GetNearestInteractable();
        //Null check
        if (!currentInteractable)
        {
            return;
        }
        //Already held, check
        if (currentInteractable.activeHand)
        {
            currentInteractable.activeHand.Drop();
        }
        //Position
        currentInteractable.ApplyOffset(transform);

        //Attach
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;
        //set active hand
        currentInteractable.activeHand = this;
    }

    public void Drop()
    {
        // Null check
        if (!currentInteractable)
        {
            return;
        }
        //Apply velocity
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();

        //detach
        joint.connectedBody = null;
        //clear
        currentInteractable.activeHand = null;
        currentInteractable = null;
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interactable interactable in contactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
