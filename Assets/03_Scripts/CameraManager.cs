using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    Transform ownTransform;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Vector3 offset;

    float targetFov = 60;

    [SerializeField]
    float moveSpeed;

    bool isFollowingPlayer = false;

    Vector3 outVelocity;
    float outVelocityFOV;

    bool isScreenShaking = false;
    float screenShakeCurrentDuration = 0.0f;
    float screenShakeDuration = 0.08f;
    float screenShakeMagnitude = 0.07f;

    void Awake()
    {
        Instance = this;

        ownTransform = transform;

        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        // TEMPORARY
        isFollowingPlayer = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(isFollowingPlayer)
        {
            Vector3 desiredPosition = playerTransform.position + offset;

            Vector3 newPosition = Vector3.SmoothDamp(ownTransform.position, desiredPosition, ref outVelocity, moveSpeed);

            ownTransform.position = newPosition;
            
            float newFOV = Mathf.SmoothDamp(mainCamera.fieldOfView, targetFov, ref outVelocityFOV, 0.5f);

            mainCamera.fieldOfView = newFOV;
        }
        if (isScreenShaking)
        {
            CameraShake();

            screenShakeCurrentDuration += Time.deltaTime;

            if (screenShakeCurrentDuration >= screenShakeDuration)
            {
                isScreenShaking = false;
            }
        }
    }

    public void ScreenshakeShock()
    {
        screenShakeCurrentDuration = 0.0f;
        screenShakeDuration = 0.05f;
        screenShakeMagnitude = 0.05f;
        isScreenShaking = true;
    }

    void CameraShake()
    {
        float x = UnityEngine.Random.Range(-1f, 1f) * screenShakeMagnitude;
        float y = UnityEngine.Random.Range(-1f, 1f) * screenShakeMagnitude;

        ownTransform.position = new Vector3(ownTransform.position.x + x, ownTransform.position.y + y, ownTransform.position.z);
    }

    public void UpdateFOV(int hatAmount)
    {
        if(hatAmount < 10)
        {
            targetFov = 60;
        }
        else
        {
            targetFov = 60 + hatAmount * 1.5f;
        }
    }
}
