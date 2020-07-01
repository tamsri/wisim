using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchanges : MonoBehaviour
{
    const float min = 55.0f;
    const float max = 100.0f;
    public float pl;
    // Update is called once per frame
    void Update()
    {
        //float pl = (float)GetComponent<winner>().pl;
        pl = (float)gameObject.GetComponentInParent<winner>().pl;
        //Debug.Log(pl.ToString());
        if (pl == 0f)
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", new Vector4(0.6f, 0.6f, 1f, .9f));
        }
        else
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", new Vector4(1.0f - (pl - min)*0.6f / (max - min), 0.2f, .2f, .9f));
        }
    }
}
