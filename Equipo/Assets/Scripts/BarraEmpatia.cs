using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraEmpatia : MonoBehaviour {

	Image imagenBarra;

	// Use this for initialization
	void Start () {
		imagenBarra = GetComponent<Image>();
		imagenBarra.fillAmount = 0;
	}

	public float darCompletitud()
	{
		return imagenBarra.fillAmount;
	}

	public void aumentar(float pAtaque)
	{
		imagenBarra.fillAmount += pAtaque;
	}

	public void reiniciar()
	{
		imagenBarra.fillAmount = 0;
	}
}
