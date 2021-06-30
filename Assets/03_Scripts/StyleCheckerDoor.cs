using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleCheckerDoor : MonoBehaviour
{
    [SerializeField]
    int hatAmounts;

    [SerializeField]
    string doorText = "";

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Color goodAnswerColor;

    [SerializeField]
    Color badAnswerColor;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        GameObject styleCheckTextGO = UIPool.Instance.GetPooledObject("StyleCheckerDoorText");

        Text styleCheckAmountText = styleCheckTextGO.GetComponent<Text>();

        if(doorText == "")
        {
            styleCheckAmountText.text = hatAmounts.ToString();
        }
        else
        {
            styleCheckAmountText.text = doorText;
        }

        styleCheckTextGO.transform.position = transform.position;

        styleCheckTextGO.SetActive(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if the player has enough hats
        if(other.gameObject.layer == 8)
        {
            CharacterController characterController = other.GetComponent<CharacterController>();

            // If he has enough, the door becomes green
            if (characterController.HasEnougHats(hatAmounts))
            {
                spriteRenderer.color = goodAnswerColor;
            }
            // Else it becomes red, and the player loses
            else
            {
                spriteRenderer.color = badAnswerColor;

                EventManager.Instance.QueueEvent(new GameFinishedEvent(false, false));
            }
        }
    }
}
