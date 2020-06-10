using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objects : MonoBehaviour
{
    public GameObject[] objects_;
    public int n = 200;
    public int seed = 312;
    // Start is called before the first frame update
    void Start()
    {
        Random.seed = seed;
        for (int m = 0; m < objects_.Length; ++m)
        {
            for (int i = 0; i < n; ++i)
            {
                GameObject new_obj = Instantiate(objects_[m], new Vector3(Random.Range(-9f, 9f), 5f, Random.Range(-9f, 9f)), Quaternion.identity);
                new_obj.transform.localScale = new Vector3(Random.Range(.1f, .13f), Random.Range(.1f, .14f), Random.Range(.1f, .15f));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
