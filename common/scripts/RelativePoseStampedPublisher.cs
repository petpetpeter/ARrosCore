/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

// Added allocation free alternatives
// UoK , 2019, Odysseas Doumas (od79@kent.ac.uk / odydoum@gmail.com)

using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class RelativePoseStampedPublisher : UnityPublisher<MessageTypes.Geometry.PoseStamped>
    {
        public Transform AnchorFrame;
        public Transform TargetFrame;
        public Text DebugText;

        /*private Transform PublishedTransform;*/
        private Vector3 PublishedPosition;
        private Quaternion PublishedRotation;
        public string FrameId = "Unity";

        private MessageTypes.Geometry.PoseStamped message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        /*private void FixedUpdate()
        {
            UpdateMessage();
        }*/

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.PoseStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    frame_id = FrameId
                }
            };
        }

        public void OnClickUpdateMessage()
        {
            message.header.Update();

            PublishedPosition = GetPositionFromAnchor();
            PublishedRotation = GetRotationFromAnchor();

            GetGeometryPoint(PublishedPosition.Unity2Ros(), message.pose.position);
            GetGeometryQuaternion(PublishedRotation.Unity2Ros(), message.pose.orientation);

            Quaternion textCheckQ = PublishedRotation.Unity2Ros();
            Vector3 textCheckV = textCheckQ.eulerAngles;
            DebugText.text = textCheckV.ToString();

            Publish(message);
        }

        private Vector3 GetPositionFromAnchor()
        {

            Vector3 targetFromWorldPosition = TargetFrame.position;
            Matrix4x4 anchorFromWorldMatrix = AnchorFrame.localToWorldMatrix;
            Matrix4x4 worldFromAnchorMatrix = anchorFromWorldMatrix.inverse;
            Vector3 targetFromAnchorPosition = worldFromAnchorMatrix.MultiplyPoint(targetFromWorldPosition);

            return targetFromAnchorPosition;
        }
        

        private Quaternion GetRotationFromAnchor()
        {
            Quaternion targetFromWorldRotation = TargetFrame.rotation;
            Quaternion anchorFromWorldRotation = AnchorFrame.rotation;
            Quaternion worldFromAnchorRotation = Quaternion.Inverse(anchorFromWorldRotation);

            Quaternion targetFromAnchorRotation = worldFromAnchorRotation * targetFromWorldRotation;


            return targetFromAnchorRotation;
        }

        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
        {
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }

    }
}
