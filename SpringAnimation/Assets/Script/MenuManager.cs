using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Définissez le nom de la scène que vous voulez charger ici.
    public string sceneToLoad;

    // Méthode appelée lorsque le bouton est cliqué.
    public void LoadSceneOnClick()
    {
        // Charge la scène avec le nom spécifié.
        SceneManager.LoadScene(sceneToLoad);
    }
}

