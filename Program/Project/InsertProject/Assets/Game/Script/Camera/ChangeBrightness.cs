using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBrightness : ObjectBase {
	private int m_BrightnessDir;  // 明るくするか暗くするか(1or-1)
	[SerializeField]
	private Light m_DirectionalLight;
	[SerializeField]
	private float m_ChangeSpeed;    // 明るさの変更速度
	[SerializeField]
	private float m_LimitBrightness;    // 暗さの最低値
	private const float m_colorVolMask = 0.003921f;
	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_BrightnessDir = -1;
		this.enabled = false;
	}

	/// <summary>
	/// 明るさチェンジ
	/// </summary>
	/// <param name="deltaTime">前フレームとの差</param>
	public override void Execute(float deltaTime) {
		var color = m_DirectionalLight.color;
		color.r += m_ChangeSpeed * m_colorVolMask * deltaTime * m_BrightnessDir;
		color.b += m_ChangeSpeed * m_colorVolMask * deltaTime * m_BrightnessDir;
		color.g += m_ChangeSpeed * m_colorVolMask * deltaTime * m_BrightnessDir;

		if(color.r < m_LimitBrightness * m_colorVolMask) {
			color.r = m_LimitBrightness * m_colorVolMask;
			color.b = m_LimitBrightness * m_colorVolMask;
			color.g = m_LimitBrightness * m_colorVolMask;
			m_DirectionalLight.color = color;
			this.enabled = false;
		}
		if(color.r > 1) {
			color.r = 1;
			color.b = 1;
			color.g = 1;
			m_DirectionalLight.color = color;
			this.enabled = false;
		}
		m_DirectionalLight.color = color;
	}

	public override void LateExecute(float deltaTime) {

	}

	public void StartChangeBrightness(int LightDir) {
		m_BrightnessDir = LightDir;
	}
}
