using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text textoDialogo;

	public GameObject botones;

	public Text[] nbPersonajes;

	public Text[] autoestimas;

	public Text nombreEnemigo;

	public Text prejuicio;

	public Text stat0;

	public Text stat1;

	public Text stat2;

	public Text statAtaque;

	public Text statVictima;

	void Awake () {

		Cursor.visible = false;

		nbPersonajes[0] = GameObject.FindGameObjectWithTag("Nombrep0").GetComponent<Text>();
		nbPersonajes[1] = GameObject.FindGameObjectWithTag("Nombrep1").GetComponent<Text>();
		nbPersonajes[2] = GameObject.FindGameObjectWithTag("Nombrep2").GetComponent<Text>();
		nbPersonajes[3] = GameObject.FindGameObjectWithTag("Nombrep3").GetComponent<Text>();

		autoestimas[0] = GameObject.FindGameObjectWithTag("Autoestimap0").GetComponent<Text>();
		autoestimas[1] = GameObject.FindGameObjectWithTag("Autoestimap1").GetComponent<Text>();
		autoestimas[2] = GameObject.FindGameObjectWithTag("Autoestimap2").GetComponent<Text>();
		autoestimas[3] = GameObject.FindGameObjectWithTag("Autoestimap3").GetComponent<Text>();

		nombreEnemigo = GameObject.FindGameObjectWithTag("NombreEnemigo").GetComponent<Text>();
		prejuicio = GameObject.FindGameObjectWithTag("Prejuicio").GetComponent<Text>();

		stat0 = GameObject.FindGameObjectWithTag("Stat0").GetComponent<Text>();
		stat1 = GameObject.FindGameObjectWithTag("Stat1").GetComponent<Text>();
		stat2 = GameObject.FindGameObjectWithTag("Stat2").GetComponent<Text>();
		statAtaque = GameObject.FindGameObjectWithTag("StatAtaque").GetComponent<Text>();
		statVictima = GameObject.FindGameObjectWithTag("StatVictima").GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setTexto(string pTexto)
	{
		textoDialogo.text = pTexto;
	}

	public void configurarEnemigo(string pNombre, string pAtaque, string pVictima)
	{
		nombreEnemigo.text = pNombre;
		statAtaque.text = pAtaque;
		statVictima.text = pVictima;
	}

	public void configurarNombres(string n1, string n2, string n3, string n4)
	{
		nbPersonajes[0].text = n1;
		nbPersonajes[1].text = n2;
		nbPersonajes[2].text = n3;
		nbPersonajes[3].text = n4;
	}

	public void actualizarAutoestimas(string a1, string a2, string a3, string a4)
	{
		autoestimas[0].text = a1 + "/50";
		autoestimas[1].text = a2 + "/50";
		autoestimas[2].text = a3 + "/50";
		autoestimas[3].text = a4 + "/50";
	}

	public void refrescarStats(float s0, float s1, string s2)
	{
		//stat0.text = s0 + " puntos";
		stat1.text = "x" + s1;
		stat2.text = s2;
	}

	public void actualizarPrejuicio(string pPrejuicio)
	{
		prejuicio.text = pPrejuicio;
	}

	public void deshabilitarBotones()
	{
		botones.SetActive(false);
	}

	public void habilitarBotones()
	{
		botones.SetActive(true);
	}
}
