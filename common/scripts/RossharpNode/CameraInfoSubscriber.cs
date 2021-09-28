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

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class CameraInfoSubscriber : UnitySubscriber<MessageTypes.Sensor.CameraInfo>
    {
        public double[] intrinsic = new double[9];
        public bool isMessageReceived; // from private to public


        protected override void Start()
        {
            base.Start();
        }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.CameraInfo cameraInfo)
        {
            intrinsic = cameraInfo.K;
            /*double fx = intrinsic[0];
            double fy = intrinsic[4];
            double cx = intrinsic[2];
            double cy = intrinsic[5];
            Debug.Log(string.Format("fx: {0} fy: {1} cx: {2} cy:{3} real", fx, fy, cx, cy));*/
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            
            isMessageReceived = false;
        }

    }
}

