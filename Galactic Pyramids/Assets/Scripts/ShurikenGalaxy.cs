
using Adobe.Substance;
using Adobe.Substance.Runtime;
using UnityEngine;

public class ShurikenGalaxy : MonoBehaviour
{
    private SubstanceRuntimeGraph runtimeGraph;

    public SubstanceGraphSO[] graphs = new SubstanceGraphSO[2];

    // Declare a variable to store the matariel rotation angle
    float angle = 0f;

    // Index of the current graph
    private int currentGraph = 0;

    void Start()
    {

        //   UpdateSubstance();
    }

    void Update()
    {
        // If the user presses space, switch to the next graph
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Increment the index and wrap around if necessary
            currentGraph++;
            if (currentGraph >= graphs.Length)
                currentGraph = 0;

            // Attach the new graph object to the runtime handler
            runtimeGraph.AttachGraph(graphs[currentGraph]);

            // Render the new graph
            runtimeGraph.Render();
        }

        // transform.Rotate(new Vector3(0f, 0.07f, 0.0f));

        // Increase the angle by 1 degree every frame
        // Set the value of the exposed float parameter that controls the rotation

        //     UpdateSubstance();

    }
    /*
        public void UpdateSubstance()
        {
            SGG.SetInputFloat("Angle", angle);
            angle += (angle + 1f) % 360f;
        }

        */
}
/*

void Update()
{
    // If the user presses space, switch to the next graph
    if (Input.GetKeyDown(KeyCode.Space))
    {
        // Increment the index and wrap around if necessary
        currentGraph++;
        if (currentGraph >= graphs.Length)
            currentGraph = 0;

        // Attach the new graph object to the runtime handler
        runtimeGraph.AttachGraph(graphs[currentGraph]);

        // Render the new graph
        runtimeGraph.Render();
    }
}
*/