using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsManager : MonoBehaviour {

	private Image pantallaNegra;
	// Use this for initialization
	void Start () {
		pantallaNegra = GetComponent<Image>();
		fadeIn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void fadeIn()
	{
		StartCoroutine("Desaparecer");
	}

	public void fadeOut()
	{
		StartCoroutine("Aparecer");
		StartCoroutine("Desaparecer");
	}

	private IEnumerator Desaparecer()
    {
        for (float i = 0; i <= 1; i = i + 0.1f)
        {
            pantallaNegra.color = new Color(pantallaNegra.color.r, pantallaNegra.color.g, pantallaNegra.color.b, 1 - i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator Aparecer()
    {
        for (float i = 0; i <= 1; i = i + 0.1f)
        {
            pantallaNegra.color = new Color(pantallaNegra.color.r, pantallaNegra.color.g, pantallaNegra.color.b, i);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
