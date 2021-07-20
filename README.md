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
python P_segmentWithRGB.py
```

3. PointCloud Handle (#FileServer/#DownSampling/#TF)
> on Omen : bring up robot
```
roslaunch pcl_ros P_camera_voxel_grid.launch
```



## Unity Side
