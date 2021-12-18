using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public GameObject Victory;
    public GameObject Fail;
    public GameObject enemyBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().isDead = true;
            //enemyAIÖÐÒÆ³ý
            enemyBehaviour.GetComponent<EnemyBehaviour>().players.Remove(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().isDead = true;
            //enemyAIÖÐÒÆ³ý
            enemyBehaviour.GetComponent<EnemyBehaviour>().enemys.Remove(other.gameObject);
        }
    }
}
