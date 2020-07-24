using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class points : MonoBehaviour
{
    public GameObject point;
    public GameObject transmitter;
    public double frequency_in_GHz = 2.4;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -100; i < 100; ++i) {
            for (int j = -100; j < 100; ++j) {
                GameObject new_obj = Instantiate(point, new Vector3(i, .5f, j), Quaternion.identity);
                new_obj.GetComponent<point>().frequency = frequency_in_GHz;
                new_obj.GetComponent<point>().transmitter = transmitter;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
