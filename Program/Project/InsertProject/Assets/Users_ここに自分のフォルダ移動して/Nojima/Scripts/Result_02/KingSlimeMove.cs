using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeMove : MonoBehaviour
{
    const float MAX_POS_Z = -1060f;
    float ExpansionSpeed = 800f;
    float POS_Z = 0f;
    float ScaleSize = 1f;
    [SerializeField]
    ResultManager CResultManager;

    /// <summary>
    /// 拡大
    /// </summary>
    public void Expansion()
    {
        if(POS_Z >= MAX_POS_Z)
            POS_Z -= ExpansionSpeed * Time.deltaTime;
        transform.localPosition = new Vector3(0f, 0f, POS_Z);

        if (POS_Z <= MAX_POS_Z)
            Small();
    }

    /// <summary>
    /// 縮小
    /// </summary>
    void Small()
    {
        if (ScaleSize >= 0f)
            ScaleSize -= 0.4f * Time.deltaTime;
        transform.localScale = new Vector2(ScaleSize, ScaleSize);

        if (ScaleSize <= 0f)
        {
            gameObject.SetActive(false);
            CResultManager.bResultMenu = true;
        }
    }
}
