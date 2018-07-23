using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorEnemigo : MonoBehaviour {

	public Image PanelUno;

	public Image PanelDos;

	int numeroDuelo = 1;

	public Color color1;

	public Color color2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void avanzarDuelo()
	{
		numeroDuelo++;
		if(numeroDuelo == 2)
		{
			PanelUno.color = color1;
			PanelDos.color = color1;
				
		}

		if(numeroDuelo == 3)
		{
			PanelUno.color = color2;
			PanelDos.color = color2;
				
		}
	}
}
