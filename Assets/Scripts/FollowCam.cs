using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject player;
    public float translationSpeed = 2f;
    
    [Tooltip("Positional confines to clamp the camera's position to")]
    public Vector3 minimumPos;
    [Tooltip("Positional confines to clamp the camera's position to")]
    public Vector3 maximumPos;
    
    private Vector3 _initOffset;

    void Awake()
    {
        _initOffset = transform.position - player.transform.position;

        if (minimumPos.x > maximumPos.x)
        {
            (minimumPos.x, maximumPos.x) = (maximumPos.x, minimumPos.x);
        }

        if (minimumPos.z > maximumPos.z)
        {
            (minimumPos.z, maximumPos.z) = (maximumPos.z, minimumPos.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerLocation = player.transform.position + _initOffset;

        Vector3 newCamPosition = new Vector3(
            Mathf.Clamp(
                playerLocation.x,
                minimumPos.x,
                maximumPos.x
            ),
            _initOffset.y,
            Mathf.Clamp(
                playerLocation.z,
                minimumPos.z,
                maximumPos.z
            )
        );

        transform.position = Vector3.Slerp(
            transform.position,
            newCamPosition,
            Time.deltaTime * translationSpeed
        );
    }
}
