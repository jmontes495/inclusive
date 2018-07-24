using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POPersonaje : MonoBehaviour {

    public enum Accion {Atacar, Defender, Poder};

	public string nombre;

	public int identificador;

	private bool vulnerable;

	private float multiplicador;

	private string defensa;

	private float autoestima;

	private Accion accionActual;

	public Image imagenPersonaje;

	public Color normal;

	public Color enTurno;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void cambiarEnTurno(bool turno)
	{
		if(turno)
		{
			imagenPersonaje.color = enTurno;
		}
		else
		{
			{
			imagenPersonaje.color = normal;
		}
		}
	}

	public string darNombre()
	{
		return nombre;
	}

	public float darAutoestima()
	{
		return autoestima;
	}

	public float darMultiplicador()
	{
		return multiplicador;
	}

	public string darDefensa()
	{
		return defensa;
	}

	public void configurar(bool pVulnerable, float pMult, string pDefensa)
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
			autoestima -= pAtaque;
			ataque = pAtaque;
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

	public Accion darAccionActual()
	{
		return accionActual;
	}


}
