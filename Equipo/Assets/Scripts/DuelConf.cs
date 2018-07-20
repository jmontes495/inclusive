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

	private EffectsManager eManager;

	private bool enEspera = false;
	// Use this for initialization
	void Start () {
		personajes = GetComponents<POPersonaje>();
		enemigoActual = GetComponent<POEnemigo>();
		interfaz = FindObjectOfType<UIManager>();
		eManager = FindObjectOfType<EffectsManager>();
		configure();
		}
	
	// Update is called once per frame
	void Update () {
		if(enEspera && enDuelo && Input.GetKeyDown(KeyCode.Space))
		{
			enEspera = false;
			interfaz.habilitarBotones();
			personajeActual = 0;
			interfaz.setTexto("¿Qué hará " + personajes[personajeActual].darNombre() + "?");
			interfaz.refrescarStats(personajes[personajeActual].darAutoestima(), personajes[personajeActual].darMultiplicador(), personajes[personajeActual].darDefensa());
		}
		if(!enDuelo && enEspera && Input.GetKeyDown(KeyCode.Space))
		{
			eManager.fadeOut();
			configure();
		}
				
	}

	public void nextDuel()
	{
		numeroDuelo++;
		enDuelo = true;
	}

	public void configure()
	{
		string victima = "";
		if(numeroDuelo == 1)
		{
			// Vulnerable, multiplicador, defensa
			personajes[0].configurar(true, 0.2f, 2.0f);
			victima = personajes[0].darNombre();
			personajes[1].configurar(false, 1.5f, 0.15f);
			personajes[2].configurar(false, 1.0f, 0.5f);
			personajes[3].configurar(false, 1.0f, 0.5f);
			enemigoActual.configurar("Tía Homofóbica", 105.5f, 30f);
		}
		interfaz.configurarNombres(personajes[0].darNombre(), personajes[1].darNombre(), personajes[2].darNombre(), personajes[3].darNombre());
		interfaz.actualizarAutoestimas(personajes[0].darAutoestima() + "", personajes[1].darAutoestima() + "", 
									personajes[2].darAutoestima() + "", personajes[3].darAutoestima() + "");
		

		interfaz.configurarEnemigo(enemigoActual.darNombre(), enemigoActual.darAtaque() + "", victima);
		interfaz.actualizarPrejuicio(enemigoActual.darPrejuicio() + "");
		interfaz.setTexto("¿Qué hará " + personajes[personajeActual].darNombre() + "?");
		interfaz.habilitarBotones();
		interfaz.refrescarStats(personajes[personajeActual].darAutoestima(), personajes[personajeActual].darMultiplicador(), personajes[personajeActual].darDefensa());
			
		enEspera = false;
		enDuelo = true;

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
			interfaz.refrescarStats(personajes[personajeActual].darAutoestima(), personajes[personajeActual].darMultiplicador(), personajes[personajeActual].darDefensa());
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
       				texto += p0.darNombre() + " se quedó sin autoestima... \nFIN DEL JUEGO";
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
		interfaz.actualizarAutoestimas(personajes[0].darAutoestima() + "", personajes[1].darAutoestima() + "", 
									personajes[2].darAutoestima() + "", personajes[3].darAutoestima() + "");
		interfaz.actualizarPrejuicio(enemigoActual.darPrejuicio() + "");
		personajeActual = 0;
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
