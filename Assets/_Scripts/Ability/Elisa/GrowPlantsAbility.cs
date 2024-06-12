using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GrowPlantsAbility : MonoBehaviour
{
    public Transform startPoint;
    public List<Transform> knots;
    public Transform endPoint;

    public List<GameObject> plantPrefabs;
    public float plantGrowDuration = 1f;
    public float plantGrowSize = 1f;
    public float plantGrowDelay = 0.5f;
    public int plantAmount = 40;

    private SplineContainer splineContainer;
    private GameObject[] plantsInstances;
    private bool plantsGrown = false;

    // Start is called before the first frame update
    void Start()
    {
        splineContainer = GetComponent<SplineContainer>();
        if (splineContainer == null)
        {
            splineContainer = gameObject.AddComponent<SplineContainer>();
        }
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Start and end points are required for the spline to be created.");
            return;
        }



        Spline spline = new Spline();
        spline.Add(new BezierKnot(startPoint.position), TangentMode.AutoSmooth);
        foreach (Transform knot in knots)
        {
            BezierKnot knotToAdd = new BezierKnot(knot.position);
            spline.Add(knotToAdd, TangentMode.AutoSmooth);
        }
        spline.Add(new BezierKnot(endPoint.position), TangentMode.AutoSmooth);

        splineContainer.Spline = spline;
    }

    public void placePlantsAlongSpline()
    {
        if (plantPrefabs.Count == 0)
        {
            Debug.LogError("No plant prefabs have been assigned.");
            return;
        }
        if (plantsGrown)
        {
            return;
        }
        plantsGrown = true;
        Spline spline = splineContainer.Spline;
        plantsInstances = new GameObject[plantAmount];

        for (int i = 0; i < plantAmount; i++)
        {
            float t = (float)i / (plantAmount - 1);
            Vector3 position = spline.EvaluatePosition(t);
            Quaternion rotation = Quaternion.LookRotation(spline.EvaluateTangent(t));
            GameObject plantInstance = Instantiate(plantPrefabs[Random.Range(0, plantPrefabs.Count)], position, rotation);
            plantsInstances[i] = plantInstance;
            StartCoroutine(growPlant(plantInstance, i));
        }
    }

    IEnumerator growPlant(GameObject plantInstance, int index)
    {
        

        Animator animator = plantInstance.GetComponent<Animator>();
        if (animator != null)
        {
            yield return new WaitForSeconds(plantGrowDelay * index);
            animator.SetTrigger("Grow");
        } else {
            Vector3 initialScale = plantInstance.transform.localScale;
            plantInstance.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(plantGrowDelay * index);
            float elapsedTime = 0;
            while (elapsedTime < plantGrowDuration)
            {
                plantInstance.transform.localScale = Vector3.Lerp(Vector3.zero, initialScale * plantGrowSize, elapsedTime / plantGrowDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            plantInstance.transform.localScale = initialScale * plantGrowSize;
        }
    }


}
