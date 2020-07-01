using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class compute : MonoBehaviour
{
    public GameObject Emitter;
    public double receivePower;
    public float transmittPower = 1;
    public int Gr = 2;
    public int Gt = 2;
    public float lamda = 0.0833f;
    public double r;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
    }
    void RayCast()
    {
        Vector3 fromPos = transform.position;
        Vector3 toPos = Emitter.transform.position;
        Vector3 direction = toPos - fromPos;
        Debug.DrawLine(fromPos, toPos);
        r = Vector3.Distance(fromPos,toPos);
        RaycastHit hit;
        if (Physics.Raycast(fromPos, direction, out hit))
        {
            receivePower = transmittPower * Gr * Gt * Math.Pow((lamda / (4 * Math.PI * r)), 2);
            Debug.Log(receivePower);
            Debug.Log("Log Scale: " + Math.Log10(receivePower)*10);
        }
        else
        {
            Debug.Log("Out of LOS");
        }
    }
}
