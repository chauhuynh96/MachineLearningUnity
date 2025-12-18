using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentController : Agent
{
    [SerializeField] private Transform m_TargetTransform;
    

    public override void OnEpisodeBegin()
    {

        transform.localPosition = new Vector3(Random.Range(-3.9f, +5.6f), 3.8f, Random.Range(-8.5f, -12f));
        m_TargetTransform.localPosition = new Vector3(Random.Range(-3.9f, +5.6f), 3.8f, Random.Range(-8.5f, -12f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(m_TargetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 10f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goal"))
        {
            SetReward(+2.0f);
            EndEpisode();
            Debug.Log("Dung ne");
        }
        if (other.CompareTag("Wall"))
        {
            SetReward(-1.0f);
            EndEpisode();
            Debug.Log("Dung roi nha");
        }
    }
}
