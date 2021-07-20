# PointCloudToAR


## ROS Side
1. Open camera (#roscore/#camera)
```
roscore
roslaunch realsense2_camera rs_rgbd_w640.launch
```

2. Open Dectection
```
source ~/Documents/mmdvenv/bin/activate
roscd realsense2_camera/scripts/
python prem_detection_drl-rgbd_01.py
```

3. PointCloud Handle (#DownSampling/#TF)
```

```

4. Run FileServer


## Unity Side
