using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WallBehavior : MonoBehaviour
{
    private GridNodeDirectional startnode;
    [SerializeField] GameObject player;
    [SerializeField] private float initialDistance;
    [SerializeField] private float wallSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float cooldown;
    private float varSpeed;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector2(player.transform.position.x - initialDistance, transform.position.y);
        varSpeed = wallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + varSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        if (transform.position.x > player.transform.position.x)
        {
            Restart();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");

            Restart();
        }
    }

    IEnumerator BufferTime()
    {
        yield return new WaitForSeconds(cooldown);
        varSpeed = wallSpeed;
        Debug.Log(varSpeed);
    }

    public void RushMode()
    {
        if (transform.position.x < (player.transform.position.x - initialDistance))
        {
            transform.position = new Vector2(player.transform.position.x - initialDistance, 0);
        }
        varSpeed = boostSpeed;
    }

    public void Restart()
    {
        Debug.Log("Restart Grid");
        startnode = player.GetComponent<GridStateMachine>().GetStartNode();
        player.GetComponent<GridStateMachine>().IdleSwap();


        player.GetComponent<GridStateMachine>().SetNode(startnode);
        transform.position = new Vector2(player.transform.position.x - initialDistance, transform.position.y);
        varSpeed = 0;
        Debug.Log(varSpeed);
        StartCoroutine(BufferTime());
        

    }

}
