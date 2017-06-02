using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectBG : MonoBehaviour
{
    [SerializeField]
    Camera MainCam;

    float BGWidth;  //背景横幅

    // Use this for initialization
    void Start()
    {
        BGWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float fFocalLength = focalLength(MainCam.fieldOfView, -BGWidth);

        MainCam.transform.position = new Vector3(transform.position.x, transform.position.y, fFocalLength);
    }

    /// <summary>
    /// 焦点距離
    /// </summary>
    /// <param name="fov">視野角</param>
    /// <param name="ObjWidth">画面幅いっぱいに表示したいオブジェクトの幅</param>
    /// <returns></returns>
    float focalLength(float fov, float ObjWidth)
    {
        float fHalfTheFOV = fov / 2.0f * Mathf.Deg2Rad;

        float fFocalLength = (0.5f / (Mathf.Tan(fHalfTheFOV) / ObjWidth));

        fFocalLength *= ((float)Screen.height / (float)Screen.width);

        return fFocalLength;
    }
}
