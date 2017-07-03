
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	CSpecialCalculation.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	曲線制御
//	
//==================================================
//	作成日：2017/06/04
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 曲線管理クラス
public class CSpecialCalculation : MonoBehaviour {

	/// <summary>
	/// エルミート曲線計算
	/// </summary>
	/// <param name="startPoint">開始地点</param>
	/// <param name="endPoint">終了地点</param>
	/// <param name="startVector">開始ベクトル</param>
	/// <param name="endVector">終了ベクトル</param>
	/// <param name="Time">経過時間</param>
	/// <returns>ポジション</returns>
	static public Vector3 HermiteCurve(Vector3 startPoint, Vector3 endPoint, Vector3 startVector, Vector3 endVector, float Time) {
		//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
		//地味に長いエルミート計算群
		float h00, h01, h10, h11;
		float t;
		t = Time;
		h00 = ( 2 * t * t * t ) - ( 3 * t * t ) + 1;
		h01 = ( -2 * t * t * t ) + ( 3 * t * t );
		h10 = ( t * t * t ) - ( 2 * t * t ) + t;
		h11 = ( t * t * t ) - ( t * t );

		Vector3 point = new Vector3(
		   //X座標の計算
		   h00 * startPoint.x + h01 * endPoint.x +
		   h10 * startVector.x + h11 * endVector.x,
		   //Y座標の計算
		   h00 * startPoint.y + h01 * endPoint.y +
		   h10 * startVector.y + h11 * endVector.y,
		   //Z座標の計算
		   h00 * startPoint.z + h01 * endPoint.z +
		   h10 * startVector.z + h11 * endVector.z);

		return point;  //求めた座標
	}

	/// <summary>
	/// ベジェ曲線
	/// </summary>
	/// <param name="startPoint">開始地点</param>
	/// <param name="endPoint">終了地点</param>
	/// <param name="firstPoint">制御点</param>
	/// <param name="Time">経過時間</param>
	/// <returns></returns>
	static public Vector3 BezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 firstPoint, float Time) {
		Vector3 point = new Vector3(
		Mathf.Pow(Time, 2) * endPoint.x + 2 * ( 1 - Time ) * Time * firstPoint.x + Mathf.Pow(( 1 - Time ), 2) * startPoint.x,
		 Mathf.Pow(Time, 2) * endPoint.y + 2 * ( 1 - Time ) * Time * firstPoint.y + Mathf.Pow(( 1 - Time ), 2) * startPoint.y,
		 Mathf.Pow(Time, 2) * endPoint.z + 2 * ( 1 - Time ) * Time * firstPoint.z + Mathf.Pow(( 1 - Time ), 2) * startPoint.z
		 );

		return point;
	}

	static public Vector3 BezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 firstPoint, Vector3 secondPoint, float Time) {
		Vector3 point;

		return point = new Vector3(
			0,
			0,
			0
		);
	}
}
