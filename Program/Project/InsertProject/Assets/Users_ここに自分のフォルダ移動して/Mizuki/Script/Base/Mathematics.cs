//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	Mathematics.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//  ベクトル系の計算詰め込みセット
//	
//==================================================
//	作成日：2017/02/01
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mathematics : SingletonMonoBehaviour<Mathematics> {
    /// <summary>
    /// 内積の計算
    /// </summary>
    /// <param name="first">一つ目のベクトル</param>
    /// <param name="second">二つ目のベクトル</param>
    /// <returns>内積</returns>
    public static float Dot(Vector3 first, Vector3 second) {
        return (first.x * second.x + first.y * second.y + first.z * second.z);
    }

	/// <summary>
	/// 座標からベクトルの計算
	/// </summary>
	/// <param name="first">開始地点</param>
	/// <param name="second">終了地点</param>
	/// <returns>ベクトル</returns>
	public static Vector3 VectorCalculation(Vector3 first, Vector3 second) {
        return new Vector3(second.x - first.x, second.y - first.y, second.z - first.z);
    }

	/// <summary>
	/// ベクトルの大きさ
	/// </summary>
	/// <param name="vector">ベクトル</param>
	/// <returns>ベクトルの大きさ</returns>
	public static float VectorSize(Vector3 vector) {
        return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
    }

	/// <summary>
	/// 2点間のベクトルの大きさ
	/// </summary>
	/// <param name="first">開始地点</param>
	/// <param name="second">終了地点</param>
	/// <returns>大きさ</returns>
	public static float VectorSize(Vector3 first, Vector3 second) {
        return Mathf.Sqrt(Mathf.Pow(( second.x - first.x ), 2) + Mathf.Pow(( second.y - first.y ), 2) + Mathf.Pow(( second.z - first.z ), 2));
    }

	/// <summary>
	/// 2ベクトルのなす角
	/// </summary>
	/// <param name="first">開始地点</param>
	/// <param name="second">終了地点</param>
	/// <returns>角度</returns>
	public static float VectorRange(Vector3 first, Vector3 second) {
		float Range = 0;

		float cos = Dot(first, second) / ( VectorSize(first) * VectorSize(second) );

		Range = Mathf.Acos(cos);

		Range *= Mathf.Rad2Deg;

		return Range;
	}
}
