using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExecute : MonoBehaviour
{
    public float attack;
    public Transform executePoint;
    public LayerMask targetLayer;
    public GameObject executeIcon;
    public bool isExecute;

    private Animator anim;
    private GameObject target;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CanExecute();
    }

    void CanExecute()
    {
        float direction = transform.localScale.x;
        Vector3 Dir = new Vector3(direction * 0.5f, 0, 0);
        RaycastHit2D executeRay = Physics2D.Raycast(executePoint.position, Dir, 0.5f, targetLayer);

        if (executeRay.collider != null)
        {
            Debug.DrawLine(executePoint.position, executeRay.point, Color.red);
            target = executeRay.collider.gameObject;

            if (executeRay.collider.tag == "Enemy")
            {
                if (executeRay.collider.GetComponent<EnemyGetHit>().isVertigo == true)
                {
                    //提示处决
                    executeIcon.SetActive(true);

                    if (Input.GetMouseButton(1) && isExecute == false)
                    {
                        isExecute = true;
                        anim.SetTrigger("execute");
                    }
                }
            }
            else if (executeRay.collider.tag == "BOSS")
            {
                if (executeRay.collider.GetComponent<EvilWizardGetHit>().isVertigo == true)
                {
                    //提示处决
                    executeIcon.SetActive(true);

                    if (Input.GetMouseButton(1) && isExecute == false)
                    {
                        isExecute = true;
                        anim.SetTrigger("execute");
                    }
                }
            }

        }
        else
        {
            Debug.DrawLine(executePoint.position, executePoint.position + Dir, Color.green);
            target = null;
            executeIcon.SetActive(false);
        }
    }

    public void Execute()
    {
        if(target.tag=="Enemy")
            target.GetComponent<EnemyGetHit>().GetExecute(attack * 10);
        else if(target.tag == "BOSS")
            target.GetComponent<EvilWizardGetHit>().GetExecute(attack * 10);
    }

    public void TryExplode()
    {
        if (target.tag == "Enemy")
            target.GetComponent<EnemyGetHit>().Explode();
        else if (target.tag == "BOSS")
            target.GetComponent<EvilWizardGetHit>().Explode();
        isExecute = false;
    }
}
