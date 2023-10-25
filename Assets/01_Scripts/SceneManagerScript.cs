using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;
    private void Awake()
    {
        instance = this;
    }
    public void CargarEscenaPrincipal()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void CargarEscenaIntro()
    {
        SceneManager.LoadScene("EscenaMenu");
    }
}
