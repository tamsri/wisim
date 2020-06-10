using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raybounce : MonoBehaviour
{
    public int max_h_ray = 20;
    public int max_v_ray = 20;
    public int max_bounce = 3;
    public float max_distance = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int h_step = 180 / max_h_ray;
        int v_step = 180 / max_v_ray;
        for (int i = 0; i< max_h_ray; ++i)
        {
            for (int j = 0; j < max_v_ray; ++j)
            {
                Vector3 h_angle = Quaternion.AngleAxis((i-max_h_ray/2) * h_step, Vector3.up) * transform.forward;
                Vector3 v_angle = Quaternion.AngleAxis((j-max_v_ray/2) * v_step , new Vector3(1, 0, 0)) * h_angle;
                CastRay(transform.position,  v_angle);
            }
            
        }
        max_distance += 0.2f;
    }
    void CastRay(Vector3 position, Vector3 direction)
    {

        for (int i = 0; i < max_bounce; ++i)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, max_distance, 1))
            {
                Debug.DrawLine(position, hit.point, new Color(.6f + 0.4f * i / max_bounce, .4f, .4f, 0.3f));
                position = hit.point;
                direction = hit.normal;
            }
            else
            {
                if (i == 0)
                {
                    Debug.DrawRay(position, direction * max_distance, new Color(.5f, .75f, .5f, 0.3f));

                }
                else
                {
                    Debug.DrawRay(position, direction * max_distance, new Color(.6f + 0.4f * i / max_bounce, .4f, .6f, 0.2f));
                }
                break;
            }
        }
    }
}
