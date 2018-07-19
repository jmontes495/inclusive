using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POPersonaje : MonoBehaviour {

    public enum Accion {Atacar, Defender, Poder};

	public string nombre;

	public int identificador;

	private bool vulnerable;

	private float multiplicador;

	private float defensa;

	private float autoestima;

	private Accion accionActual;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string darNombre()
	{
		return nombre;
	}

	public float darAutoestima()
	{
		return autoestima;
	}

	public void configurar(bool pVulnerable, float pMult, float pDefensa)
	{
		vulnerable = pVulnerable;
		multiplicador = pMult;
		defensa = pDefensa;
		autoestima = 50f;
	}

	public float reducirAutoestima(float pAtaque)
	{
		float ataque = 0;
		if(vulnerable || accionActual == Accion.Defender)
		{
			ataque = (pAtaque * defensa);
			autoestima -= ataque;
		}
		if(autoestima < 0)
		{
			autoestima = 0;
		}

		return ataque;
	}

	public void definirAccion(Accion pAccion)
	{
		accionActual = pAccion;
	}

	public int defiende()
	{
		int defensor = 0;
		if(accionActual == Accion.Defender)
		{
			defensor = 1;
		}
		return defensor;
	}

	public float ataca()
	{
		float ataque = 0f;
		if(accionActual == Accion.Atacar)
		{
			ataque = 10 * multiplicador;
		}
		else if(accionActual == Accion.Poder && vulnerable)
		{
			ataque = 50f;
		}

		return ataque;
	}


}
