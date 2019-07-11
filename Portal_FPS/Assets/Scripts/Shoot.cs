using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Transform cameraTransform;
    private GameObject firstTeleporter;
    private GameObject secondTeleporter;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 start = cameraTransform.position + cameraTransform.right * 0.1f;
            Vector3 end = cameraTransform.position + cameraTransform.forward * 100;

            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit))
            {
                end = hit.point;
                if (hit.transform.tag == "teleport")
                {
                    Debug.Log("Teleport");
                    if (firstTeleporter == null)
                    {
                        CreateTeleporter(hit.point, out firstTeleporter);
                    }
                    else
                    {
                        Debug.Log("Destroy");
                        Destroy(secondTeleporter);
                        secondTeleporter = firstTeleporter;
                        CreateTeleporter(hit.point, out firstTeleporter);
                    }
                    
                }
            }

            GameObject resource = Resources.Load("Beam") as GameObject;
            BeamManager beam = resource.GetComponent<BeamManager>();
            beam.start = start;
            beam.end = end;

            Instantiate(resource);
        }
    }

    private void CreateTeleporter(Vector3 position, out GameObject teleporter)
    {
        teleporter = Resources.Load("teleporter") as GameObject;
        Instantiate(teleporter, position, Quaternion.identity);
    }

}
