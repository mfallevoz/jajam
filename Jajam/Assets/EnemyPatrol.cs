using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
 public float speed;
 public Transform[] waypoints;

 private Transform target;
 private int destPoint;
 public SpriteRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        //change rend flip x to true if enemy is to the left of the player
        if(target.position.x < transform.position.x){
            rend.flipX = true;
        }else{
            rend.flipX = false;
        }
       

        if (Vector2.Distance(transform.position, target.position) < 0.3f)
        {
            
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            
        }else{
           
        }
        {
            
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collided with " + col.name);
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Controller>().die();
        }
    }
}
