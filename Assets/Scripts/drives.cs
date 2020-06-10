using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drives : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.1f;
    private int desination;
    void Start()
    {
        desination = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        switch (desination) {
            case 1:
                transform.position += new Vector3(-speed, 0f, 0f);
                break;
            case 2:
                transform.position += new Vector3(0f, 0f, speed);
                break;
            case 3:
                transform.position += new Vector3(0f, 0f, -speed);
                break;
            default:
                transform.position += new Vector3(speed, 0f, 0f);
                break;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.5f, 1))
        {
            desination = Random.Range(1, 5);
            Debug.Log(desination);
        }
    }
}
