
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;
	[HideInInspector] public Vector3 pos { get { return transform.position; } }
	
	//public int score = 0;
    public Score score;
    public GameObject objectSpawner;
    private Dictionary<GameObject, float> recentCollisions = new Dictionary<GameObject, float>();
    private float cooldownTime = 2f;
    
    private bool isPaused = false;
    private bool modInfinite= false;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<CircleCollider2D> ();
	}

	public void Push (Vector2 force)
	{
		rb.AddForce (force, ForceMode2D.Impulse);
	}

	public void ActivateRb ()
	{
		rb.isKinematic = false;
	}

	public void DesactivateRb ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}


    void Update()
    {
        ProcesarInput();
        // Remover objetos que ya hayan pasado el tiempo de espera
        List<GameObject> expiredCollisions = new List<GameObject>();

        foreach (var collision in recentCollisions)
        {
            if (Time.time - collision.Value > cooldownTime)
            {
                expiredCollisions.Add(collision.Key);
            }
        }

        // Limpiar el diccionario de colisiones pasadas
        foreach (var expired in expiredCollisions)
        {
            recentCollisions.Remove(expired);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto ya está en la lista de colisiones recientes, ignorarlo
        if (recentCollisions.ContainsKey(collision.gameObject))
            return;

        // Añadir el objeto a la lista de colisiones recientes con el tiempo actual
        recentCollisions[collision.gameObject] = Time.time;

        // Detectar tipo de objeto y ajustar puntuación
        if (collision.gameObject.CompareTag("Positive"))
        {
            //score += 10;  // Sumar puntos
            score.AddScore(10); // Sumar puntos
            if(modInfinite){
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Negative"))
        {
            //score -= 5;  // Restar puntos
            score.AddScore(-10); // Restar puntos
            if(modInfinite){
                Destroy(collision.gameObject);
            }
        }
    }
    
    void ProcesarInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleBooleanVariable();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // Pausa o reanuda el juego
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena actual
    }

    void ToggleBooleanVariable()
    {
        objectSpawner.SetActive(!objectSpawner.activeInHierarchy);
        modInfinite= !modInfinite;
    }
}
