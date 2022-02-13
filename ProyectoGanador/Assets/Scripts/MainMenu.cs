using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject optionsFirstButton = null, optionsUI = null, mainUI = null; // Referencian el menú de opciones y el botón seleccionado al abrirlo

    private void Start() // Inicia la música del menú
    {
        //AudioManager.instance.Play(AudioManager.ESounds.Menu);        
    }

    public void Play() // Para la música del menú y carga la primera escena
    {
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

    public void Quit() // Cierra el juego
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Options() // Abre el menú de opciones
    {
        optionsUI.SetActive(true);
        mainUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
}
