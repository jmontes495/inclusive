using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POEnemigo : MonoBehaviour {

	private string nombre;

	private float prejuicio = 0;

	private float ataque = 0;
	// Use this for initialization
	void Start () {
		
	}

	public POEnemigo()
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void configurar(string pNombre, float pPrejuicio, float pAtaque)
	{
		nombre = pNombre;
		prejuicio = pPrejuicio;
		ataque = pAtaque;
	}

	public void reducirPrejuicio(float pAtaque)
	{
		prejuicio -= pAtaque;
		if(prejuicio < 0)
		{
			prejuicio = 0;
		}
	}

	public string darNombre()
	{
		return nombre;
	}

	public float darAtaque()
	{
		return ataque;
	}

	public float darPrejuicio()
	{
		return prejuicio;
	}
}
