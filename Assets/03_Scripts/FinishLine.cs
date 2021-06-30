using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    List<ParticleSystem> particles;

    [SerializeField]
    List<Animator> publicAnimators;

    bool playerCrossedFinishLine = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!playerCrossedFinishLine)
        {
            if (other.gameObject.layer == 8)
            {
                playerCrossedFinishLine = true;

                EventManager.Instance.QueueEvent(new GameFinishedEvent(true));

                for (int i = 0; i < particles.Count; i++)
                {
                    particles[i].Play();
                }

                for (int i = 0; i < publicAnimators.Count; i++)
                {
                    publicAnimators[i].Play("Clap", 0, 0.0f);
                }
            }
        }
    }
}
