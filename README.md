# PointCloudToAR


## ROS Side
1. Open camera (#roscore/#camera)
```
roscore
roslaunch realsense2_camera rs_rgbd_w640.launch
```
> Azure
```
roscore
roslaunch azure_kinect_ros_driver driver.launch
```

2. Open Dectection
```
source ~/Documents/mmdvenv/bin/activate
roscd realsense2_camera/scripts/
python P_segmentWithRGB.py
python P_cropdepth.py
```

> Azure
```
conda activate azure_ros
roscd azure_marker/scripts
python marker.py
python cropDepthImage.py
```
```
roslaunch azure_marker markerTF.launch
```

3. PointCloud Handle (#FileServer/#DownSampling/#TF)
> on Omen : bring up robot
> hostname : hostname -I
```
roslaunch pcl_ros P_camera_voxel_grid.launch
```



## Unity Side
>build xxxWithPc.scene
