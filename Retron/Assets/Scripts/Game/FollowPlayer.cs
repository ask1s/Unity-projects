using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public Vector3 offset;

    private int dir = 1;

    private Quaternion rotation;
    private Quaternion current;
    private Vector3 currentPos;
    private Vector3 targetPos;
    private Vector3 vel = Vector3.zero;
    private bool rotating = false;
    private int ticker = 0;

    private void Start()
    {
        offset.Set(0, 1.02f, -3f);
        transform.position = player.transform.position + offset;
    }

    void Update()
    {
        Quaternion rotation;
        Quaternion current;
        Vector3 currentPos;
        Vector3 targetPos;

        if (rotating)
        {
            currentPos = transform.position;
            targetPos = player.transform.position + offset;
            ticker++;
            if (ticker <= 210)
            {
               transform.position = Vector3.Lerp(currentPos, targetPos, 1.5f * Time.deltaTime);
            }
            else if (ticker > 210 && ticker < 270)
            {
               transform.position = Vector3.Lerp(currentPos, targetPos, 20 * Time.deltaTime);
            }
            else
            {
                ticker = 0;
                rotating = false;
            }
        }
        else
        {
            transform.position = player.transform.position + offset;
        }

        Vector3 distance = player.transform.position - transform.position;

        rotation = Quaternion.LookRotation(distance);
        current = transform.rotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, 3f * Time.deltaTime);
    }

    public void SetDir(int dir)
    {
        this.dir = dir;
        rotating = true;        
        switch (dir)
        {
            case 1:
                offset.Set(0, 1.02f, -3f);
                break;

            case 2:
                offset.Set(-3f, 1.02f, 0);
                break;

            case 3:
                offset.Set(0, 1.02f, 3f);
                break;

            case 4:
                offset.Set(3f, 1.02f, 0);
                break;
        }
    }
}
