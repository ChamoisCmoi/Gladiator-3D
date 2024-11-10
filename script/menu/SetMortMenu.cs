using UnityEngine;

public class SetMortMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelMort;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("mort"))
        {
            panelMort.SetActive(true);
            PlayerPrefs.DeleteKey("mort");
        }
        else
        {
            panelMort.SetActive(false);
        }
    }
}
