using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandsSequence : MonoBehaviour
{
    public List<Transform> positions;  // List of empty GameObjects representing the sequence of positions
    public float moveInterval = 1.0f;  // Time interval between movements in seconds

    private int currentIndex = 0;
    private Coroutine moveCoroutine;
    public JackLevelCompletion LevelCompletion;

    void Start()
    {
        if (positions.Count > 0)
        {
            // Start the coroutine to move the GameObject
            moveCoroutine = StartCoroutine(MoveToNextPosition());
        }
    }

    IEnumerator MoveToNextPosition()
    {
        while (true)
        {
            // Move the GameObject to the current position in the sequence
            transform.position = positions[currentIndex].position;

            // Increment the index and wrap around if necessary
            currentIndex = (currentIndex + 1) % positions.Count;

            // Wait for the specified interval before moving to the next position
            yield return new WaitForSeconds(moveInterval);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop the coroutine to stop the movement
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                LevelCompletion.RemoveHand(this);
            }
        }
    }
}
