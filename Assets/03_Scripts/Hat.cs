using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    [SerializeField]
    BoxCollider ownCollider;

    [SerializeField]
    Rigidbody ownRigidbody;

    Transform ownTransform;

    bool hasBeenPickedUp = false;           // Has the hat been picked up by the player?
    bool isWorn = false;                    // Is the hat currently worn by the player?

    CharacterController owner = null;

    void Awake()
    {
        ownTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasBeenPickedUp)
        {
            ownTransform.Rotate(Vector3.up, 300.0f * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Hat not picked up collected by a player
        if(!hasBeenPickedUp && other.gameObject.layer == 8)
        {
            CharacterController characterController = other.GetComponent<CharacterController>();

            owner = characterController;

            PickUp(characterController);
        }
        // If the hat is currently worn by the player and hits an obstacle
        else if(isWorn && other.gameObject.layer == 10)
        {
            RemoveHat(true);
        }
    }

    private void PickUp(CharacterController owner)
    {
        owner.AddHat(this);

        hasBeenPickedUp = true;
        isWorn = true;
        ownTransform.eulerAngles = Vector3.zero;
    }

    public void RemoveHat(bool byObstacle)
    {
        isWorn = false;

        // Set back collision and gravity when the hat falls
        transform.parent = null;
        ownCollider.isTrigger = false;
        ownRigidbody.isKinematic = false;
        ownRigidbody.useGravity = true;
        
        owner.RemoveHat(this, byObstacle);
    }

    public bool IsCurrentlyWorn()
    {
        return isWorn;
    }
}
