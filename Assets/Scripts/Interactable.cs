using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    [HideInInspector]
    public Hand activeHand = null;

    public virtual void Action()
    {
        print("Action!");
    }

    public void ApplyOffset(Transform hand)
    {
        transform.SetParent(hand);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = offset;
        transform.SetParent(null);
    }
}
