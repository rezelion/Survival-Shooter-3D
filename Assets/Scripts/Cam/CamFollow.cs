using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;
    // Start is called before the first frame update
    private void Start()
    {
        // Mendaptakan offset antara target dan camera
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Mendapatkan posisi untuk camera
        Vector3 targetCamPos = target.position + offset;

        //set pos camera dengan smoothing
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
