using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DuelConf : MonoBehaviour {

	private int numeroDuelo = 1;

	private bool enDuelo;

	private int personajeActual = 0;

	private POPersonaje[] personajes;

	private POEnemigo enemigoActual;

	private UIManager interfaz;

	private EffectsManager eManager;

	private bool enEspera = false;

	private BarraEmpatia barraEmpatia;

	private string victima;

	public GameObject botonEmpatia;

	public GameObject botonAtaque;

	private EventSystem _eventSystem;

	private float maximo;

	private ColorEnemigo colorEnemigo;
	// Use this for initialization
	void Start () {
		personajes = GetComponents<POPersonaje>();
		enemigoActual = GetComponent<POEnemigo>();
		interfaz = FindObjectOfType<UIManager>();
		eManager = FindObjectOfType<EffectsManager>();
		barraEmpatia = FindObjectOfType<BarraEmpatia>();
		_eventSystem = FindObjectOfType<EventSystem>();
		colorEnemigo = FindObjectOfType<ColorEnemigo>();
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
			activarBarra();
			StartCoroutine("SelectContinueButtonLater");
		}
		if(!enDuelo && enEspera && Input.GetKeyDown(KeyCode.Space))
		{
			eManager.fadeOut();
			configure();
		}
				
	}

	public void configure()
	{
		victima = "";
		if(numeroDuelo == 1)
		{
			// Vulnerable, multiplicador, defensa
			personajes[0].configurar(false, 1.0f, "Gay mestizo");
			personajes[1].configurar(false, 0.8f, "Negra");
			personajes[2].configurar(false, 1.5f, "Trans blanca");
			personajes[3].configurar(true, 0.2f, "Pobre");
			victima = personajes[3].darNombre();
			enemigoActual.configurar("Cajero elitista", 180f, 20f);
			maximo = 180f;
		}
		else if(numeroDuelo == 2)
		{
			// Vulnerable, multiplicador, defensa
			colorEnemigo.avanzarDuelo();
			personajes[0].configurar(false, 0.8f, "Gay mestizo");
			personajes[1].configurar(true, 0.2f, "Negra");
			personajes[2].configurar(false, 1.5f, "Trans blanca");
			personajes[3].configurar(false, 1.0f, "Pobre");
			victima = personajes[1].darNombre();
			enemigoActual.configurar("Cuñado racista", 200f, 20f);
			maximo = 200;
		}
		else if(numeroDuelo == 3)
		{
			// Vulnerable, multiplicador, defensa
			personajes[0].configurar(true, 0.2f, "Gay mestizo");
			victima = personajes[0].darNombre();
			personajes[1].configurar(false, 1.5f, "Negra");
			personajes[2].configurar(false, 0.8f, "Trans blanca");
			personajes[3].configurar(false, 1.0f, "Pobre");
			enemigoActual.configurar("Tía Homofóbica", 200f, 20f);
			maximo = 200f;
		}
		

		interfaz.configurarNombres(personajes[0].darNombre(), personajes[1].darNombre(), personajes[2].darNombre(), personajes[3].darNombre());
		interfaz.actualizarAutoestimas(personajes[0].darAutoestima() + "", personajes[1].darAutoestima() + "", 
									personajes[2].darAutoestima() + "", personajes[3].darAutoestima() + "");
		

		interfaz.configurarEnemigo(enemigoActual.darNombre(), enemigoActual.darAtaque() + "", victima);
		interfaz.actualizarPrejuicio(enemigoActual.darPrejuicio()  + "/" + maximo);
		interfaz.setTexto("¿Qué hará " + personajes[personajeActual].darNombre() + "?");
		interfaz.habilitarBotones();
		interfaz.refrescarStats(personajes[personajeActual].darAutoestima(), personajes[personajeActual].darMultiplicador(), personajes[personajeActual].darDefensa());
			
		enEspera = false;
		enDuelo = true;
		barraEmpatia.reiniciar();
		activarBarra();
		StartCoroutine("SelectContinueButtonLater");
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
			activarBarra();
			StartCoroutine("SelectContinueButtonLater");
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
    	int ataque = 0;
    	foreach (POPersonaje p0 in personajes)
    	{
       		enemigoActual.reducirPrejuicio(p0.ataca());
       		enemigoSufre += p0.ataca();
       		if(p0.darAccionActual() == POPersonaje.Accion.Atacar)
       		{
       			ataque++;
       		}
       		else if(p0.darAccionActual() == POPersonaje.Accion.Poder)
       		{
    			texto += "Con el apoyo de sus amigos " + victima + " generó 50 de daño \n";
    			barraEmpatia.reiniciar();
       		}
    	}
    	barraEmpatia.aumentar(ataque*0.2f);
    	texto += "El enemigo pierde " + enemigoSufre + " de prejuicio \n";
    	texto += "El equipo adquiere " + ataque + " puntos de empatía \n";

		if(enemigoActual.darPrejuicio() > 0)
		{
			// Generar el daño sobre los miembros del equipo
			foreach (POPersonaje p0 in personajes)
    		{
       			float damage = p0.reducirAutoestima(enemigoActual.darAtaque()/(defensa + 1));
       			if(damage != 0)
       			{
       				texto += p0.darNombre() + " sufre " + damage + " de daño \n";
       			}
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
			numeroDuelo++;
			texto += "Has acabado con el prejuicio! :)";
		}

		interfaz.setTexto(texto);
		interfaz.actualizarAutoestimas(personajes[0].darAutoestima() + "", personajes[1].darAutoestima() + "", 
									personajes[2].darAutoestima() + "", personajes[3].darAutoestima() + "");
		interfaz.actualizarPrejuicio(enemigoActual.darPrejuicio() + "/" + maximo);
		personajeActual = 0;
		interfaz.deshabilitarBotones();
		StartCoroutine("Espera");			
	}



	public void activarBarra()
	{
		if(barraEmpatia.darCompletitud() >= 1 && personajes[personajeActual].darNombre().Equals(victima))
		{
			botonEmpatia.SetActive(true);
		}
		else
		{
			botonEmpatia.SetActive(false);
		}
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
		yield return new WaitForSeconds(0.25f);
		enEspera = true;
	}

	private IEnumerator SelectContinueButtonLater()
     {
         yield return null;
         _eventSystem.SetSelectedGameObject(null);
         _eventSystem.SetSelectedGameObject(botonAtaque.gameObject);
     }


}
