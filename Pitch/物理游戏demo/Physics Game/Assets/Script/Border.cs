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
            //enemyAI中移除
            enemyBehaviour.GetComponent<EnemyBehaviour>().players.Remove(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().isDead = true;
            //enemyAI中移除
            enemyBehaviour.GetComponent<EnemyBehaviour>().enemys.Remove(other.gameObject);
        }
        //如果摄像头跟随则换视角
        if (followCam.GetComponent<CinemachineVirtualCamera>().Follow == other.transform) 
        {
            followCam.GetComponent<CinemachineVirtualCamera>().Follow =
                enemyBehaviour.GetComponent<EnemyBehaviour>().players[0].transform;
        }
        //死亡图标
        other.GetComponent<ObjImage>().image.GetComponent<TeamButton>().ObjDead();
    }
}
