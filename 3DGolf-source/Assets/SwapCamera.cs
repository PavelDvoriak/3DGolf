using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamera : MonoBehaviour
{
    public Camera camTop;
    public Camera cam3D;

    private float cameraAngle;

    // Start is called before the first frame update
    void Start()
    {
        camTop.gameObject.SetActive(true);
        cam3D.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cameraAngle += Input.GetAxis("Horizontal") * 100f * Time.deltaTime;

        if (cam3D.isActiveAndEnabled)
        {
            cam3D.transform.rotation = Quaternion.Euler(30, cameraAngle, 0);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(camTop.isActiveAndEnabled)
            {
                camTop.gameObject.SetActive(false);
                cam3D.gameObject.SetActive(true);
            } else if(cam3D.isActiveAndEnabled)
            {
                cam3D.gameObject.SetActive(false);
                camTop.gameObject.SetActive(true);
            } else
            {
                Debug.Log("No active camera? How did it happen?");
            }

        }
    }
}
