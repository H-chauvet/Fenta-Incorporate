using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveObject : MonoBehaviour
{
    // D�finir le tag du personnage autoris�
    public string authorizedTag = "Character1";
    private InputAction interact;

    private void Start()
    {
        interact = InputSystem.actions.FindAction("Interact");
    }
    void OnTriggerStay(Collider other)
    {
        // V�rifier si l'objet entrant a le tag autoris�
        if (other.CompareTag(authorizedTag))
        {
            // Appeler la fonction d'interaction
            Interact();
        }
    }

    void Interact()
    {
        // Logique d'interaction ici
        // D�sactiver l'objet
        if (interact.IsPressed() )
        {
            Debug.Log("Interaction is a success " + authorizedTag);
            gameObject.SetActive(false);
        }
    }
}
