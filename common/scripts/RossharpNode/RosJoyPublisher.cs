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
using System.Text;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class RosJoyPublisher : UnityPublisher<MessageTypes.Sensor.Joy>
    {

        public string FrameId = "Unity";

        private MessageTypes.Sensor.Joy message;

        public VariableJoystick variableJoystick;
        private int num_axis = 2;
        private int num_button = 2;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void Update()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Sensor.Joy();
            message.header.frame_id = FrameId;
            message.axes = new float[2];
            message.buttons = new int[2];
        }

        private void UpdateMessage()
        {
            Debug.Log(variableJoystick.Direction);
            message.header.Update();

            for (int i = 0; i < num_axis; i++)
                message.axes[i] = variableJoystick.Direction[i];

            for (int i = 0; i < num_button; i++)
                message.buttons[i] = 0;

            Publish(message);
        }
    }
}
