using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // D�finissez le nom de la sc�ne que vous voulez charger ici.
    public string sceneToLoad;

    // M�thode appel�e lorsque le bouton est cliqu�.
    public void LoadSceneOnClick()
    {
        // Charge la sc�ne avec le nom sp�cifi�.
        SceneManager.LoadScene(sceneToLoad);
    }
}

