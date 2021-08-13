# PickAndPlace


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
python P_masking_to_posearray.py
```

3. TF ()
> on Omen : bring up robot
> hostname : hostname -I
```
roslaunch pcl_ros P_camera_voxel_grid.launch
```



## Unity Side
>build xxxWithPc.scene
