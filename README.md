# JoyoAR


## ROS Side
1. Open camera (#roscore/#camera)
```
roslaunch turtlebot3_gazebo turtlebot3_world.launch
```

2. Open Dectection
```
source ~/Documents/mmdvenv/bin/activate
roscd realsense2_camera/scripts/
python P_segmentWithRGB.py
```

3. PointCloud Handle (#FileServer/#DownSampling/#TF)
> on Omen : bring up robot
> hostname : hostname -I
```
roslaunch pcl_ros P_camera_voxel_grid.launch
```



## Unity Side
>build xxxWithPc.scene
