using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Border : MonoBehaviour
{
    public GameObject Victory;
    public GameObject Fail;
    public GameObject enemyBehaviour;
    public GameObject followCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().isDead = true;
            //enemyAI���Ƴ�
            enemyBehaviour.GetComponent<EnemyBehaviour>().players.Remove(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().isDead = true;
            //enemyAI���Ƴ�
            enemyBehaviour.GetComponent<EnemyBehaviour>().enemys.Remove(other.gameObject);
        }
        //�������ͷ�������ӽ�
        if (followCam.GetComponent<CinemachineVirtualCamera>().Follow == other.transform) 
        {
            followCam.GetComponent<CinemachineVirtualCamera>().Follow =
                enemyBehaviour.GetComponent<EnemyBehaviour>().players[0].transform;
        }
        //����ͼ��
        other.GetComponent<ObjImage>().image.GetComponent<TeamButton>().ObjDead();
    }
}
