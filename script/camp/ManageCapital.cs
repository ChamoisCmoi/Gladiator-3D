using UnityEngine;
using TMPro;

public class ManageCapital : MonoBehaviour
{
    [Header("variables")]
    [SerializeField] int certerces;
    [SerializeField] int experience;

    [Header("à rattacher")]
    [SerializeField] TMP_Text textCerterces;
    [SerializeField] TMP_Text textExperience;

    #region getteur
    public int GetCerterces()
    {
        return certerces;
    }
    public int GetExperience()
    {
        return experience;
    }
    #endregion

    #region add
    public void AddCerterces(int enPlus)
    {
        certerces += enPlus;
        ActualizeAffichage();
    }
    public void AddExperience(int enPlus)
    {
        experience += enPlus;
        ActualizeAffichage();
    }
    #endregion

    #region setteurs
    public void SetCerterces(int value)
    {
        certerces = value;
        ActualizeAffichage();
    }
    public void SetExperience(int value)
    {
        experience = value;
        ActualizeAffichage();
    }
    #endregion



    void ActualizeAffichage()
    {
        textCerterces.text = certerces.ToString();
        textExperience.text = experience.ToString();

        PlayerPrefs.SetInt("certerces", certerces);
        PlayerPrefs.SetInt("experience", experience);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        ActualizeAffichage();
    }
    private void Awake()
    {
        certerces = PlayerPrefs.GetInt("certerces", 50);
        experience = PlayerPrefs.GetInt("experience", 0);
    }
}
