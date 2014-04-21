using UnityEngine;
using System.Collections;


    public class GunMenu : MonoBehaviour
    {
        //灵敏度.
        public float sensitivityX = 15;
        public float sensitivityY = 15;
        //xy方向大最大最小旋转值.
        public float minimumX = -360;
        public float maximumX = 360;
        public float minimumY = -60;
        public float maximumY = 60;

        public float delayMouse = 3;//鼠标延时.

        //降噪.
        public float noiseX = 0.1f;
        public float noiseY = 0.1f;
        public bool Noise;
        //呼吸影响。
        public float breathHolderVal = 5;


        private float rotationX = 0;
        private float rotationY = 0;
        private float rotationXtemp = 0;
        private float rotationYtemp = 0;
        private Quaternion originalRotation;
        private float noisedeltaX;
        private float noisedeltaY;
        private float stunY;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            sensitivityY = sensitivityX;
           
            
                //计算浮动值.
                noisedeltaX += ((((Mathf.Cos(Time.time) * Random.Range(-10, 10) / 5f) * noiseX) - noisedeltaX) / 100) * breathHolderVal;
                noisedeltaY += ((((Mathf.Sin(Time.time) * Random.Range(-10, 10) / 5f) * noiseY) - noisedeltaY) / 100) * breathHolderVal;
            

            //计算xy方向的浮动值.
            rotationXtemp += noisedeltaX;
            rotationYtemp += noisedeltaY;
            rotationX += (rotationXtemp - rotationX);//计算x延时.

            //x方向的角度控制在360到-360之间.
            if (rotationX >= 360)
            {
                rotationX = 0;
                rotationXtemp = 0;
            }
            if (rotationX <= -360)
            {
                rotationX = 0;
                rotationXtemp = 0;
            }

            rotationY += (rotationYtemp - rotationY) / delayMouse;//计算y延时.

            ////得到旋转角度.
            //rotationX = ClampAngle(rotationX, minimumX, maximumX);
            //rotationY = ClampAngle(rotationY, minimumY, maximumY);


            //计算旋转.
            var xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            var yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);

            //得到旋转值.
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;

        }



        //控制角度.
        static float ClampAngle(float angle, float min, float max)
        {

            if (angle < -360.0f)
                angle += 360.0f;

            if (angle > 360.0f)
                angle -= 360.0f;

            return Mathf.Clamp(angle, min, max);

        }
    }


