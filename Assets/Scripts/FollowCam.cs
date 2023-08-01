using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject player;
    private Vector3 initOffset;
    public float cameraSmoothness = 0.3f;

    // Start is called before the first frame update
    void Awake()
    {
        initOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(player.transform.position - transform.position),
            Time.deltaTime
        );
    }
}
