using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

	private EfectosSonido efectosSonido;

	private string fraseFinal;

	private string fraseInicial;
	// Use this for initialization
	void Start () {
		personajes = GetComponents<POPersonaje>();
		enemigoActual = GetComponent<POEnemigo>();
		interfaz = FindObjectOfType<UIManager>();
		eManager = FindObjectOfType<EffectsManager>();
		barraEmpatia = FindObjectOfType<BarraEmpatia>();
		_eventSystem = FindObjectOfType<EventSystem>();
		colorEnemigo = FindObjectOfType<ColorEnemigo>();
		efectosSonido = FindObjectOfType<EfectosSonido>();
		configure();
		}
	
	// Update is called once per frame
	void Update () {
		if(enEspera && enDuelo && Input.GetKeyDown(KeyCode.Space))
		{
			enEspera = false;
			interfaz.habilitarBotones();
			personajeActual = 0;
			interfaz.setTexto("¿What will " + personajes[personajeActual].darNombre() + " do?");
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
			fraseInicial = "The group of friends go get something to eat but the cashier looks with disgust at Pedro and informs them the dinner has the right of admission reserved.";
			personajes[0].configurar(false, 1.0f, "Gay, mixed race");
			personajes[1].configurar(false, 0.8f, "Black");
			personajes[2].configurar(false, 1.5f, "Trans, white");
			personajes[3].configurar(true, 0.2f, "Poor");
			victima = personajes[3].darNombre();
			enemigoActual.configurar("Elitist cashier", 180f, 20f);
			fraseFinal = "The manager apologized in the name of the restaurant, since their policy is not that. The cashier looks embarrassed. You defeated prejudice!";
			maximo = 180f;
		}
		else if(numeroDuelo == 2)
		{
			// Vulnerable, multiplicador, defensa
			fraseInicial = "The family of Mariana's boyfriend invites them for dinner. His brother asks if he got the maid pregnant.";
			colorEnemigo.avanzarDuelo();
			personajes[0].configurar(false, 0.8f, "Gay, mixed race");
			personajes[1].configurar(true, 0.2f, "Black");
			personajes[2].configurar(false, 1.5f, "Trans, white");
			personajes[3].configurar(false, 1.0f, "Poor");
			victima = personajes[1].darNombre();
			enemigoActual.configurar("Racist brother in law", 200f, 20f);
			fraseFinal= "In our family, love has always crossed barriers. The brother in law realizes that differences in the color of the skin should be celebrated, not frowned upon. You Defeated prejudice!";
			maximo = 200;
		}
		else if(numeroDuelo == 3)
		{
			// Vulnerable, multiplicador, defensa
			fraseInicial = " Eduardo has a very religious family, but it is time to come out of the closet.";
			personajes[0].configurar(true, 0.2f, "Gay, mixed race");
			victima = personajes[0].darNombre();
			personajes[1].configurar(false, 1.5f, "Black");
			personajes[2].configurar(false, 0.8f, "Trans, white");
			personajes[3].configurar(false, 1.0f, "Poor");
			enemigoActual.configurar("Catholic homophobic aunt", 200f, 20f);
			fraseFinal="The aunt looks at her bible, reflects and says that it is true, we cannot love God unless we love each other. You defeated prejudice!";
			maximo = 200f;
		}
		else if(numeroDuelo == 4)
		{
			SceneManager.LoadScene("Final");
		}
		

		interfaz.configurarNombres(personajes[0].darNombre(), personajes[1].darNombre(), personajes[2].darNombre(), personajes[3].darNombre());
		interfaz.actualizarAutoestimas(personajes[0].darAutoestima() + "", personajes[1].darAutoestima() + "", 
									personajes[2].darAutoestima() + "", personajes[3].darAutoestima() + "");
		

		interfaz.configurarEnemigo(enemigoActual.darNombre(), enemigoActual.darAtaque() + "", victima);
		interfaz.actualizarPrejuicio(enemigoActual.darPrejuicio()  + "/" + maximo);
			interfaz.setTexto("¿What will " + personajes[personajeActual].darNombre() + " do?");
		interfaz.habilitarBotones();
		interfaz.refrescarStats(personajes[personajeActual].darAutoestima(), personajes[personajeActual].darMultiplicador(), personajes[personajeActual].darDefensa());
			
		enEspera = true;
		enDuelo = true;
		barraEmpatia.reiniciar();
		activarBarra();
		personajes[personajeActual].cambiarEnTurno(false);
		interfaz.setTexto(fraseInicial);
		interfaz.deshabilitarBotones();
		StartCoroutine("SelectContinueButtonLater");
	}

	public void avanzarPersonaje()
	{
		personajes[personajeActual].cambiarEnTurno(false);
		personajeActual++;
		if(personajeActual > 3)
		{
			enemigoAtaca();
		}
		else
		{
			personajes[personajeActual].cambiarEnTurno(true);
			interfaz.setTexto("¿What will " + personajes[personajeActual].darNombre() + " do?");
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
    			texto += "With the support of friends " + victima + " striked 50 points of damage \n";
    			barraEmpatia.reiniciar();
       		}
    	}
    	barraEmpatia.aumentar(ataque*0.2f);
    	texto += "The enemy loses " + enemigoSufre + " of prejudice \n";
    	texto += "The team wins " + ataque + " empathy points \n";

		if(enemigoActual.darPrejuicio() > 0)
		{
			// Generar el daño sobre los miembros del equipo
			foreach (POPersonaje p0 in personajes)
    		{
       			float damage = p0.reducirAutoestima(enemigoActual.darAtaque()/(defensa + 1));
       			if(damage != 0)
       			{
       				texto += p0.darNombre() + " suffers " + damage + " points of damage \n";
       			}
       			if(p0.darAutoestima() == 0)
       			{
       				enDuelo = false;
       				texto += p0.darNombre() + " is out of self esteem... \nGAME OVER";
       				break;
       			}
    		}
		}
		else
		{
			enDuelo = false;
			numeroDuelo++;
			texto += fraseFinal;
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
		efectosSonido.elegir();
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
