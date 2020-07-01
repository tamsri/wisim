using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatefog : MonoBehaviour
{
    public GameObject fog;
    public GameObject transmitter;
    public float frequency_in_GHz;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = -50; i < 50; i += 1)
            for (int j = -50; j < 50; j += 1)
            {
                GameObject new_obj = Instantiate(fog, new Vector3(i, 1.5f, j), Quaternion.identity);
                new_obj.GetComponent<winner>().frequency = frequency_in_GHz;
                new_obj.GetComponent<winner>().transmitter = transmitter;
            }
    }

}
