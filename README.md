# PointCloudToAR


## ROS Side
1. Open camera (#roscore/#camera)
```
roscore
roslaunch rs_marker rsOdom.launch
```
> Azure
```
roscore
roslaunch azure_kinect_ros_driver driver.launch
```

2. Open Dectection
```
conda activate armesh
roscd azure_marker/scripts/ 
python azureMarker.py
```
```
conda activate armesh
roscd azure_marker/scripts/ 
python cropDepthImage.py
```
```
roslaunch pointcloud_transformer azurePC.launch
```

3. PointCloud Handle (#FileServer/#DownSampling/#TF)
> on Omen : bring up robot


4.motomanIK
```
roslaunch motoman_gazebo sia5_basic_moveit_gazebo.launch
```
```
roscd azure_marker/scripts
python sia5IK.py
```
## Unity Side
>build xxxWithPc.scene
