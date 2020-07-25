using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchanges : MonoBehaviour
{
    public const float min = 50f;
    public const float max = 103f;
    public float pl;
    // Update is called once per frame
    void Update()
    {
        //float pl = (float)GetComponent<winner>().pl;
        pl = (float)gameObject.GetComponentInParent<winner>().pl;
        //Debug.Log(pl.ToString());
        GetComponent<MeshRenderer>().material.SetColor("_Color", GetHeatColor(pl));
    }
    Vector4 GetHeatColor(float value) {
        if (value == -1f) return new Vector4(1f, 1f, 1f, 1f);
        if (value < min || value > max) return new Vector4(0f,0f,0f,1f);
        float r, g, b;
        float nv = (value - min) / (max - min);
        if(nv<0.5f){
            r = -2*nv+1f;
            b = 0;
        }else{
            r = 0;
            b = 2*nv-1f;
        }
        g = (1-r-b);
        return new Vector4(r, g, b, 1f);
    }
}
