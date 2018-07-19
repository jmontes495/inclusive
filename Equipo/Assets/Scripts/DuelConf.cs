using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelConf : MonoBehaviour {

	private int numeroDuelo = 1;

	private bool enDuelo;

	private int personajeActual = 0;

	private POPersonaje[] personajes;

	private POEnemigo enemigoActual;

	private UIManager interfaz;

	private bool enEspera = false;
	// Use this for initialization
	void Start () {
		personajes = GetComponents<POPersonaje>();
		enemigoActual = GetComponent<POEnemigo>();
		interfaz = FindObjectOfType<UIManager>();
		configure();
		enEspera = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(enEspera && Input.GetKeyDown(KeyCode.Space))
		{
			enEspera = false;
			interfaz.habilitarBotones();
			personajeActual = 0;
			interfaz.setTexto("¿Qué hará " + personajes[personajeActual].darNombre() + "?");
		}
	}

	public void nextDuel()
	{
		numeroDuelo++;
		enDuelo = true;
	}

	public void configure()
	{
		if(numeroDuelo == 1)
		{
			// Vulnerable, multiplicador, defensa
			personajes[0].configurar(true, 0.2f, 2.0f);
			personajes[1].configurar(false, 1.5f, 0.15f);
			personajes[2].configurar(false, 1.0f, 0.5f);
			personajes[3].configurar(false, 1.0f, 0.5f);
			enemigoActual.configurar(105.5f, 10f);
			interfaz.setTexto("¿Qué hará " + personajes[personajeActual].darNombre() + "?");
		}
	}

	public void avanzarPersonaje()
	{
		personajeActual++;
		if(personajeActual > 3)
		{
			enemigoAtaca();
		}
		else
		{
			interfaz.setTexto("¿Qué hará " + personajes[personajeActual].darNombre() + "?");
		}
	}

	public void enemigoAtaca()
	{
		// Determinar la defensa del equipo
		int defensa = 0;
		string texto = "";
		foreach (POPersonaje p0 in personajes)
    	{
       		defensa += p0.defiende();
    	}

    	// Generar el daño sobre el enemigo
    	float enemigoSufre = 0;
    	foreach (POPersonaje p0 in personajes)
    	{
       		enemigoActual.reducirPrejuicio(p0.ataca());
       		enemigoSufre += p0.ataca();
    	}
    	texto += "El enemigo sufre " + enemigoSufre + " de daño \n";

		if(enemigoActual.darPrejuicio() > 0)
		{
			// Generar el daño sobre los miembros del equipo
			foreach (POPersonaje p0 in personajes)
    		{
       			float damage = p0.reducirAutoestima(enemigoActual.darAtaque()/(defensa + 1));
       			texto += p0.darNombre() + " sufre " + damage + " de daño \n";
       			if(p0.darAutoestima() == 0)
       			{
       				enDuelo = false;
       				texto += "se quedó sin autoestima FIN DEL JUEGO";
       				break;
       			}
    		}
		}
		else
		{
			enDuelo = false;
			texto += "Has acabado con el prejuicio! :)";
		}

		interfaz.setTexto(texto);
		//enEspera = true;
		interfaz.deshabilitarBotones();
		StartCoroutine("Espera");
			
	}

	public void definirAccion(POPersonaje.Accion pAccion)
	{
		personajes[personajeActual].definirAccion(pAccion);
		avanzarPersonaje();
	}

	public void jugadorAtaca()
	{
		definirAccion(POPersonaje.Accion.Atacar);
	}

	public void jugadorDefiende()
	{
		definirAccion(POPersonaje.Accion.Defender);
	}

	public void jugadorPoder()
	{
		definirAccion(POPersonaje.Accion.Poder);
	}

	private IEnumerator Espera()
	{
		yield return new WaitForSeconds(0.5f);
		enEspera = true;
	}


}
