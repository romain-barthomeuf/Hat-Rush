using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    Transform ownTransform;

    [SerializeField]
    Transform hatPointTransform;

    [SerializeField]
    float movementSpeed;
    
    [SerializeField]
    Animator ownAnimator;

    [SerializeField]
    NavMeshAgent navMeshAgent;

    [SerializeField]
    Transform hatAmountTextTransform;

    [SerializeField]
    Text hatAmountText;

    List<Hat> hats = new List<Hat>();

    bool canMove = false;

    void Awake()
    {
        ownTransform = transform;

        EventManager.Instance.AddListener<GameStartedEvent>(OnGameStarting);
        EventManager.Instance.AddListener<GameFinishedEvent>(OnGameEnding);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MovementUpdate();
        }
    }

    void MovementUpdate()
    {
        float horizontalInput = CnInputManager.GetAxis("Horizontal");

        Vector3 movementVector = (ownTransform.forward + Vector3.right * horizontalInput / 2.0f) * movementSpeed * Time.deltaTime;

        Vector3 possibleNewPosition = ownTransform.position + movementVector;

        possibleNewPosition.x = Mathf.Clamp(possibleNewPosition.x, -2.05f, 2.05f);

        ownTransform.position = possibleNewPosition;

        float yAngle = horizontalInput * 20.0f;

        ownTransform.localEulerAngles = new Vector3(0.0f, yAngle, 0.0f);
        hatAmountTextTransform.localEulerAngles = new Vector3(0.0f, yAngle, 0.0f);
    }
    void LateUpdate()
    {
        navMeshAgent.nextPosition = ownTransform.position;
    }

    public void AddHat(Hat hat)
    {
        // Parent the hat to the hat point and place it
        hat.transform.parent = hatPointTransform;
        hat.transform.localPosition = Vector3.zero + Vector3.up * hats.Count * 0.25f;

        hats.Add(hat);

        UpdateHatText();

        CameraManager.Instance.UpdateFOV(hats.Count);
    }
    
    public void RemoveHat(Hat hat, bool removingHatsAbove)
    {
        if (!removingHatsAbove)
        {
            hats.Remove(hat);
        }
        // If this hat has been removed by an obstacle, we make all the hats above it fall as well
        else
        {
            int index = 0;
            bool found = false;

            while (!found && index < hats.Count)
            {
                if (hats[index] == hat)
                    found = true;
                else
                   index++;
            }

            if (found)
            {
                // Remove all the hats from the top until the hat that has been hit
                for (int i = hats.Count - 1; i >= index; i--)
                {
                    hats[i].RemoveHat(false);
                }

                UpdateHatText();
            }
        }

        CameraManager.Instance.UpdateFOV(hats.Count);
    }
    
    void UpdateHatText()
    {
        if (canMove)
        {
            hatAmountText.text = hats.Count.ToString();
        }
    }

    public void Win()
    {
        ownAnimator.Play("WinDance", 0, 0.0f);

        CameraManager.Instance.UpdateFOV(0);
    }

    public void OnTriggerEnter(Collider other)
    {
        // If it's an obstacle, the player loses
        if(other.gameObject.layer == 10 && canMove)
        {
            canMove = false;

            CameraManager.Instance.ScreenshakeShock();

            EventManager.Instance.QueueEvent(new GameFinishedEvent(false, true));
        }
    }

    public void Lose(bool fromObstacle)
    {
        if (fromObstacle)
        {
            ownAnimator.Play("ObstacleLose", 0, 0.0f);

            while(hats.Count > 0)
            {
                RemoveHat(hats[0], false);
            }
        }
        else
        {
            ownAnimator.Play("CheckerLose", 0, 0.0f);
        }
    }

    private void OnGameStarting(GameStartedEvent e)
    {
        canMove = true;

        ownAnimator.SetTrigger("Walk");

        navMeshAgent.enabled = true;
        navMeshAgent.updateRotation = false;
    }

    private void OnGameEnding(GameFinishedEvent e)
    {
        canMove = false;

        hatAmountText.gameObject.SetActive(false);

        if(e.playerWon)
        {
            Win();
        }
        else
        {
            Lose(e.playerLostToObstacle);
        }
    }

    public bool HasEnougHats(int amountRequired)
    {
        return hats.Count >= amountRequired;
    }
}
