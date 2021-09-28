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
using System.Collections;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class RawImageSubscriber : UnitySubscriber<MessageTypes.Sensor.Image>
    {

        

        public byte[] imageData;
        /*public byte[] ImageData
        {
            get { return imageData; }
        }*/

        public bool isMessageReceived;

        public MeshRenderer meshRenderer;

        public Texture2D texture2D;

        int width = 1280;
        int height = 720;





        protected override void Start()
        {
            base.Start();
            texture2D = new Texture2D(width, height, TextureFormat.ARGB4444, false);
            //meshRenderer.material = new Material(Shader.Find("Standard"));
            //meshRenderer.material.mainTexture = texture;
            //rend.material.SetTextureScale ("_MainTex", new Vector2 (-1, 1));

        }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.Image imageMessage)
        {
            
            imageData = imageMessage.data;
            isMessageReceived = true;
            Debug.Log(string.Format("Reciveing Image"));
            Debug.Log(string.Format("Length: {0}", imageMessage.data.Length));
        }

        private void ProcessMessage()
        {
            /*texture2D.LoadImage(imageData);*/
            texture2D.LoadRawTextureData(imageData);
            texture2D.Apply();
            meshRenderer.material.SetTexture("_MainTex", texture2D);
            

            isMessageReceived = false;
        }
    }
}
