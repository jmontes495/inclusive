using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text textoDialogo;

	public GameObject botones;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setTexto(string pTexto)
	{
		textoDialogo.text = pTexto;
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
