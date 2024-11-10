using UnityEngine;

public class Retour : MonoBehaviour
{

    [SerializeField] GameObject objetToFerme;

    private void OnMouseDown()
    {
        objetToFerme.SetActive(false);
    }
}
