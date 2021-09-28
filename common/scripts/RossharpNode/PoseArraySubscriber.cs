using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*using RosSharp.RosBridgeClient.MessageTypes.Std;*/

namespace RosSharp.RosBridgeClient
{
    public class PoseArraySubscriber : UnitySubscriber<MessageTypes.Geometry.PoseArray>


    {
        //private MessageTypes.Geometry.Pose _pose

        public List<Transform> pickObjects;
        public Text debug1;

        private bool isMessageReceived;

        private Vector3 i_position;
        private Quaternion i_rotation;

        protected override void Start()
        {
            base.Start();
        }


        protected override void ReceiveMessage(MessageTypes.Geometry.PoseArray message)
        {
            int index;

            for (int i = 0; i < message.poses.Length; i++)
            {
                i_position = GetPosition(message.poses[i]).Ros2Unity();
                i_rotation = GetRotation(message.poses[i]).Ros2Unity();
                pickObjects[i].position = i_position;
                pickObjects[i].rotation = i_rotation;
            }
            debug1.text = i_position.ToString();
        }

        private Vector3 GetPosition(MessageTypes.Geometry.Pose message)
        {
            return new Vector3(
                (float)message.position.x,
                (float)message.position.y,
                (float)message.position.z);
        }

        private Quaternion GetRotation(MessageTypes.Geometry.Pose message)
        {
            return new Quaternion(
                (float)message.orientation.x,
                (float)message.orientation.y,
                (float)message.orientation.z,
                (float)message.orientation.w);
        }

    }
}
