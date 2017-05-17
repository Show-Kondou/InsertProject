//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//
//  [拡張機能群]
//
//  ファイル名：Extentions.cs
//  説　　　明：拡張機能クラス群
//  制　作　者：Show Kondou
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//  - 更新履歴 -
//  2012  12/14　… Transformの拡張作成
//
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using UnityEngine;



/// <summary>
/// Transformの拡張機能
/// </summary>
public static class CExtFotTransform {
	#region Method

	/// <summary>
	/// TransformのpositionのXに値を設定
	/// </summary>
	/// <param name="transform">拡張先クラス</param>
	/// <param name="x">X成分</param>
	public static void SetX( this Transform transform, float x ) {
		var pos = transform.position;
		pos.x = x;
		transform.position = pos;
	}



	/// <summary>
	/// TransformのpositionのYに値を設定
	/// </summary>
	/// <param name="transform">拡張先クラス</param>
	/// <param name="y">X成分</param>
	public static void SetY( this Transform transform, float y ) {
		var pos = transform.position;
		pos.y = y;
		transform.position = pos;
	}



	/// <summary>
	/// TransformのpositionのZに値を設定
	/// </summary>
	/// <param name="transform">拡張先クラス</param>
	/// <param name="z">Z成分</param>
	public static void SetZ( this Transform transform, float z ) {
		var pos = transform.position;
		pos.z = z;
		transform.position = pos;
	}

	#endregion Method
}