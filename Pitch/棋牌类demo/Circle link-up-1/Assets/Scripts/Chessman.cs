using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chessman : MonoBehaviour
{
    private Transform CenterPoint;
    public bool rotate;

    private Ray ray_right;
    public RaycastHit hitInfo_right;
    private Ray ray_left;
    public RaycastHit hitInfo_left;
    private Ray ray_up;
    public RaycastHit hitInfo_up;
    private Ray ray_down;
    public RaycastHit hitInfo_down;

    private Ray ray_center;
    private Ray ray_point;
    private LayerMask layer;
    private RaycastHit hitInfo_point;
    private Vector3 latestPos;

    void Start()
    {
        CenterPoint = GameObject.FindGameObjectWithTag("CenterPoint").transform;
        layer = LayerMask.NameToLayer("Chesspoint");
    }

    void Update()
    {
        if (rotate)
            RotateAround();
        DirectCenter();
        RayCast();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rotate = true;
            //记录还原位置
            latestPos = transform.position;
            //传递当前为激活棋子
            GameManager.instance.activeCM_left = gameObject;
            GameManager.instance.activeCM_right = gameObject;
            GameManager.instance.activeCM_up = gameObject;
            GameManager.instance.activeCM_down = gameObject;
            GameManager.instance.CreatTeam();
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            AdjustPosition();

            GameManager.instance.ClearTeam();
        }
    }

    public void AdjustPosition()
    {
        //射线检测位置
        ray_point = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(ray_point, out hitInfo_point, 1))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            Debug.DrawLine(transform.position, hitInfo_point.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray_point.origin, ray_point.origin + ray_point.direction, Color.green);
        }

        //如果对准了位置
        if (hitInfo_point.collider.gameObject.tag == "Chesspoint")
        {
            //移动到点位位置
            Transform point = hitInfo_point.collider.transform;
            transform.position = new Vector3(point.position.x, transform.position.y, point.position.z);
            rotate = false;
        }
        else//否则还原位置
        {
            transform.position = latestPos;
            rotate = false;
        }
    }

    //鼠标控制左右移动
    private void RotateAround()
    {
        //鼠标右移
        if (Input.GetAxis("Mouse X") > 0)
        {
            GameManager.instance.RightTeamRotate();
            transform.RotateAround(CenterPoint.position, Vector3.up, -1);
        }
        //鼠标左移
        else if (Input.GetAxis("Mouse X") < 0) 
        {
            GameManager.instance.LeftTeamRotate();
            transform.RotateAround(CenterPoint.position,Vector3.up, 1);
        }
    }

    //射线检测
    private void RayCast()
    {
        //射线检测红轴对准中心
        ray_center = new Ray(transform.position, transform.forward);
        Debug.DrawLine(ray_center.origin, ray_center.origin + ray_center.direction, Color.blue);

        //左侧射线
        ray_left = new Ray(transform.position, 1.732f * transform.forward - 3 * transform.right);
        if (Physics.Raycast(ray_left, out hitInfo_left, 0.55f))
        {
            Debug.DrawLine(transform.position, hitInfo_left.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray_left.origin, ray_left.origin + ray_left.direction, Color.green);
        }

        //右侧射线
        ray_right = new Ray(transform.position, 1.732f * transform.forward + 3 * transform.right);
        if (Physics.Raycast(ray_right, out hitInfo_right, 0.55f))
        {
            Debug.DrawLine(transform.position, hitInfo_right.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray_right.origin, ray_right.origin + ray_right.direction, Color.green);
        }

        //下侧射线
        ray_down = new Ray(transform.position, -transform.forward);
        if (Physics.Raycast(ray_down, out hitInfo_down, 0.55f))
        {
            Debug.DrawLine(transform.position, hitInfo_down.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray_down.origin, ray_down.origin + ray_down.direction, Color.green);
        }

        //上侧射线
        ray_up = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray_up, out hitInfo_up, 0.55f))
        {
            Debug.DrawLine(transform.position, hitInfo_up.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray_up.origin, ray_up.origin + ray_up.direction, Color.green);
        }
    }

    //对准红轴中心
    private void DirectCenter()
    {
        Vector3 dir = CenterPoint.position - transform.position;
        if (dir != Vector3.zero)
        {
            transform.forward = dir.normalized;
        }
    }
}
