using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour
{
    public GameObject transmitter;
    public double frequency;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FindLOS();

    }

    void FindLOS() {
        Vector3 direction = (transmitter.transform.position - transform.position);
        float distance = Vector3.Distance(transform.position, transmitter.transform.position);
        Vector3 origin = transform.position;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, distance, 1) && hit.transform.position == transmitter.transform.position) {
            Debug.DrawLine(transform.position, transmitter.transform.position);
        }
    }
}
