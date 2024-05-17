using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveObject : MonoBehaviour
{
    // Définir le tag du personnage autorisé
    public string authorizedTag = "Character1";
    private InputAction interact;

    private void Start()
    {
        interact = InputSystem.actions.FindAction("Interact");
    }
    void OnTriggerStay(Collider other)
    {
        // Vérifier si l'objet entrant a le tag autorisé
        if (other.CompareTag(authorizedTag))
        {
            // Appeler la fonction d'interaction
            Interact();
        }
    }

    void Interact()
    {
        // Logique d'interaction ici
        // Désactiver l'objet
        if (interact.IsPressed() )
        {
            Debug.Log("Interaction is a success " + authorizedTag);
            gameObject.SetActive(false);
        }
    }
}
