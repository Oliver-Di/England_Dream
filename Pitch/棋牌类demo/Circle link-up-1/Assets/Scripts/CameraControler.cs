using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    private float origanRotateX;
    private float origanTranX;
    private float targetRotateX;
    private float targetTranX;

    private float rand1;
    private float rand2;

    public float rotateSpeed;
    public float horizontalSpeed;

    void Start()
    {
        origanRotateX = targetRotateX = transform.rotation.eulerAngles.x;
        origanTranX = targetTranX = transform.position.x;
    }


    void Update()
    {
        SwagCamera();
    }

    private void SwagCamera()
    {
        if (false && transform.rotation.eulerAngles.x == targetRotateX)
        {
            ChangeRotateTarget();
        }
        else
        {
            //transform.rotation = Quaternion.Euler(Mathf.MoveTowards(transform.rotation.eulerAngles.x, targetRotateX, rotateSpeed), 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(80, 0, 0), Time.deltaTime * rotateSpeed);
        }

        if (transform.position.x != targetTranX)
        {
            transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetTranX,
                horizontalSpeed), transform.position.y, transform.position.z);
        }
        else
        {
            ChangeTransformTarget();
        }
    }

    private void ChangeRotateTarget()
    {
        rand1 = Random.Range(-2, 2);
        targetRotateX = origanRotateX + rand1;
    }

    private void ChangeTransformTarget()
    {
        rand2 = Random.Range(-0.5f, 0.5f);
        targetTranX = origanTranX + rand2;
    }
}
