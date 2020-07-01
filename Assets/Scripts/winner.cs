using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winner : MonoBehaviour
{
    public GameObject transmitter;
    public float frequency = 2.4f;
    public float hb;
    public float hm;
    public const float c = 3e8f;
    TextMesh textbar;
    public double pl;
    bool isLOS;
    float distance;
    float dl1;
    float dl2;
    float dr1;
    float dr2;

    float hbsc;
    float hmsc;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        textbar = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, transmitter.transform.position, Color.blue);
        direction =  transmitter.transform.position - transform.position;
        distance = Vector3.Distance(transform.position, transmitter.transform.position);
        hb = transmitter.transform.position.y;
        hm = transform.position.y;
        hbsc = hb - 1;
        hmsc = hm - 1;
        float dbp = 4*hbsc*hmsc*frequency*1e9f/c;
        if (distance < 10 || distance > dbp) {
            textbar.text = "The condition does not meet requirements";
            return;
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, distance, 1) && hit.transform.position == transmitter.transform.position)
        {
            pl = GetLOS(distance);
            textbar.text = "LOS: " + pl.ToString();
            //Debug.DrawLine(transform.position, transmitter.transform.position);
        }
        else
        {
            dl1 = 0.0f;dl2 = 0.0f;
            dr1 = 0.0f; dl2 = 0.0f;
            LeftPathFinding();
            RightPathFinding();
            float pl1 = 0.0f, pl2 = 0.0f, pr1 = 0.0f, pr2 = 0.0f;
            if (dl1 != 0.0f && dl2 != 0.0f) {
                pl1 = GetNLOS(dl1, dl2);
                pl2 = GetNLOS(dl2, dl1);
                
            }
            if (dr1 != 0.0f && dr2 != 0.0f)
            {
                pr1 = GetNLOS(dr1, dr2);
                pr2 = GetNLOS(dr2, dr1);
            }
            List<float> results = new List<float>(); 
            if (pl1 != 0) results.Add(pl1);
            if (pl2 != 0) results.Add(pl2);
            if (pr1 != 0) results.Add(pr1);
            if (pr2 != 0) results.Add(pr2);
            if (!results.Any()) {
                textbar.text = "Cannot trace";
                return;
            }
            pl = results.Min();
            textbar.text = "NLOS: " + pl.ToString();
        }
        
    }
    float GetLOS(float d)
    {
        return (float)(22.7f * Math.Log10(d) + 41.0f + 20.0f * Math.Log10(frequency / 5.0f));
    }
    float GetNLOS(float dk, float dl) {
        float nj = 2.8f - 0.0024f*dk;
        if (nj < 1.84f) nj = 1.84f;
        return (float)(GetLOS(dk) + 20.0f - 12.5f * nj + 10.0f * nj * Math.Log10(dl) + 3 * Math.Log10(frequency / 5.0f));
    }

    void LeftPathFinding() {
        /// Left Path Finding
        Vector3 LeftAngle = Vector3.Normalize(Quaternion.AngleAxis(0, Vector3.up) * direction);
        Vector3 RightAngle = Vector3.Normalize(Quaternion.AngleAxis(90, Vector3.up) * direction);
        for (int i = 0; i < 90; i += 1)
        {
            if (!IsMiddleHit(LeftAngle, Quaternion.AngleAxis(i, Vector3.up) * LeftAngle))
            {
                RightAngle = Quaternion.AngleAxis(i, Vector3.up) * LeftAngle;
                break;
            }
        }
        Vector3 EdgeAngle = Vector3.Normalize(GetEdgeAngle(LeftAngle, RightAngle));
        Vector3 LeftEdge = Vector3.Normalize(Quaternion.AngleAxis(-0.01f, Vector3.up) * EdgeAngle);
        Vector3 RightEdge = Vector3.Normalize(Quaternion.AngleAxis(0.01f, Vector3.up) * EdgeAngle);

        Ray check_ray = new Ray(transform.position, LeftEdge);
        float distanceToEdge = 0.0f;
        RaycastHit check_hit;
        if (Physics.Raycast(check_ray, out check_hit, distance, 1))
        {
            distanceToEdge = Vector3.Distance(transform.position, check_hit.point) + 0.05f;
            //Debug.DrawLine(transform.position, check_hit.point, Color.green);
        }
        else
        {
            return;
        }
        //Debug.DrawLine(transform.position, transform.position + RightEdge * distanceToEdge, Color.red);

        //Second ray
        Vector3 second_origin = transform.position + distanceToEdge * RightEdge;
        //Debug.DrawLine(transform.position, second_origin, Color.red);
        Vector3 second_direction = Vector3.Normalize(transmitter.transform.position - second_origin);
        float second_distance = Vector3.Distance(second_origin, transmitter.transform.position);

        Ray second_ray = new Ray(second_origin, second_direction);
        RaycastHit second_check_hit;
        if (Physics.Raycast(second_ray, out second_check_hit, second_distance, 1) && second_check_hit.transform.position == transmitter.transform.position)
        {
            //Debug.DrawLine(second_origin, second_check_hit.point, Color.yellow);
        }
        else
        {
            textbar.text = "NLOS: NOT ABLE TO CALCUALTE";
            return;
        }
        dl1 = distanceToEdge;
        dl2 = second_distance;
    }
    void RightPathFinding()
    {
        /// Left Path Finding
        Vector3 LeftAngle = Vector3.Normalize(Quaternion.AngleAxis(-90, Vector3.up) * direction);
        Vector3 RightAngle = Vector3.Normalize(Quaternion.AngleAxis(0, Vector3.up) * direction);
        for (int i = 0; i > -90; i -= 1)
        {
            if (!IsMiddleHit(RightAngle, Quaternion.AngleAxis(i, Vector3.up) * RightAngle))
            {
                LeftAngle = Quaternion.AngleAxis(i, Vector3.up) * RightAngle;
                break;
            }
        }
        Vector3 EdgeAngle = Vector3.Normalize(GetEdgeAngle(RightAngle, LeftAngle));
        Vector3 LeftEdge = Vector3.Normalize(Quaternion.AngleAxis(-0.01f, Vector3.up) * EdgeAngle);
        Vector3 RightEdge = Vector3.Normalize(Quaternion.AngleAxis(+0.01f, Vector3.up) * EdgeAngle);

        Ray check_ray = new Ray(transform.position, RightEdge);
        float distanceToEdge = 0.0f;
        RaycastHit check_hit;
        if (Physics.Raycast(check_ray, out check_hit, distance, 1))
        {
            distanceToEdge = Vector3.Distance(transform.position, check_hit.point) + 0.05f;
            //Debug.DrawLine(transform.position, check_hit.point, Color.green);
        }
        else
        {
            return;
        }
        //Debug.DrawLine(transform.position, transform.position + LeftEdge * distanceToEdge, Color.red);

        //Second ray
        Vector3 second_origin = transform.position + distanceToEdge * LeftEdge;
        //Debug.DrawLine(transform.position, second_origin, Color.red);
        Vector3 second_direction = Vector3.Normalize(transmitter.transform.position - second_origin);
        float second_distance = Vector3.Distance(second_origin, transmitter.transform.position);

        Ray second_ray = new Ray(second_origin, second_direction);
        RaycastHit second_check_hit;
        if (Physics.Raycast(second_ray, out second_check_hit, second_distance, 1) && second_check_hit.transform.position == transmitter.transform.position)
        {
            //Debug.DrawLine(second_origin, second_check_hit.point, Color.yellow);
        }
        else
        {
            textbar.text = "NLOS: NOT ABLE TO CALCUALTE";
            return;
        }
        dr1 = distanceToEdge;
        dr2 = second_distance;
    }

    Vector3 GetEdgeAngle(Vector3 LeftAngle, Vector3 RightAngle)
    {
        //Debug.DrawLine(transform.position, transform.position+ 10 * LeftAngle, Color.yellow);

        // Divide and Conqure
        const int max_itr = 18; // log2(90) == ~6 !
        Vector3 MiddleAngle = (LeftAngle + RightAngle) / 2;
        for (int i = 0; i < max_itr; i++)
        {
            MiddleAngle = (LeftAngle + RightAngle) / 2;
            if (IsMiddleHit(LeftAngle, RightAngle))
            {
                LeftAngle = MiddleAngle;
            }
            else
            {
                RightAngle = MiddleAngle;
               
            }
        }
        return MiddleAngle;
    }
    bool IsMiddleHit(Vector3 angle_left, Vector3 angle_right) //count from left
    {
        Vector3 angle_center = (angle_left + angle_right) / 2;
        Ray check_ray = new Ray(transform.position, angle_center);
        RaycastHit check_hit;
        if (Physics.Raycast(check_ray, out check_hit, distance, 1)) {
            return true;
        }
        else {
            return false;
        }
    }


}
