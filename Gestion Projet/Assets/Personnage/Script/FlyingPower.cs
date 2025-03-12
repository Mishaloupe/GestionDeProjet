using System.Collections;
using UnityEngine;

public class FlyOnCollision : MonoBehaviour
{
    public string powerUpTag = "FlyPower"; // Tag de l'objet qui donne le pouvoir de voler
    public float flyDuration = 30f;        // Dur�e du vol en secondes
    public float flySpeed = 10f;           // Vitesse de d�placement en vol

    private Rigidbody rb;
    private bool isFlying = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // R�cup�re le Rigidbody du personnage
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(powerUpTag))
        {
            StartCoroutine(FlyTemporarily(other.gameObject));
        }
    }

    private IEnumerator FlyTemporarily(GameObject powerUpObject)
    {
        Debug.Log("Pouvoir de vol activ� !");
        isFlying = true;
        rb.useGravity = false; // D�sactive la gravit�
        powerUpObject.SetActive(false); // D�sactive l'objet touch�

        float timer = 0f;
        while (timer < flyDuration)
        {
            HandleFlying(); // G�re les contr�les en vol
            timer += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Fin du vol !");
        isFlying = false;
        rb.useGravity = true; // R�active la gravit�
    }

    private void HandleFlying()
    {
        if (isFlying)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            float moveY = 0f;

            // Ajoute un contr�le vertical si le joueur appuie sur espace (monter) ou shift (descendre)
            if (Input.GetKey(KeyCode.Space)) moveY = 1f;
            if (Input.GetKey(KeyCode.LeftShift)) moveY = -1f;

            Vector3 moveDirection = new Vector3(moveX, moveY, moveZ).normalized;
            rb.velocity = moveDirection * flySpeed;
        }
    }
}
