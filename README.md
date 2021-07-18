# The unity sample of ARCore and ROS implementation with ROS#


On developoing pls w8 :)

This a set of ARCore ROS samples that provides assets using ROS#  for the comunication between ROS and Mobile node.

The version of this example are
- Unity 2019.xx LTS
- ROS Melodic on Unbuntu 18.xx LTS

## Setting Up
### Install arcore sdk for unity
1. follow quick start guide
> https://developers.google.com/ar/develop/unity/quickstart-android

2. config custom gradle (Test only version 6.5)
> https://developers.google.com/ar/develop/unity/android-11-build


![image](https://user-images.githubusercontent.com/55285546/126052974-392d5c8e-e502-4feb-9011-af7fd31f54e3.png)

> https://gradle.org/releases/

Insert the following lines at the top of the file:
```
buildscript {
    repositories {
        google()
        jcenter()
    }
    dependencies {
        // Must be Android Gradle Plugin 3.6.0 or later. For a list of
        // compatible Gradle versions refer to:
        // https://developer.android.com/studio/releases/gradle-plugin
        classpath 'com.android.tools.build:gradle:3.6.0'
    }
}

allprojects {
   repositories {
      google()
      jcenter()
      flatDir {
        dirs 'libs'
      }
   }
}
```

3. test example

![image](https://user-images.githubusercontent.com/55285546/126052090-4671cc0a-0f7a-4c69-9861-010d1f6f8bbb.png)

### Add ros# package
> https://github.com/siemens/ros-sharp
- Unity Setup
1. Copy the RosSharp folder from the latest commit of our repository into the Assets folder of your Unity project.
2. Check Installation

![image](https://user-images.githubusercontent.com/55285546/126052377-b42d6bd5-4e7a-4aa8-9eb8-6a2bc849f7f1.png)

- Ros Setup
> For WSL-ROS installation : https://jack-kawell.com/2020/06/12/ros-wsl2/
0. Create your workspace   : https://github.com/gmp-prem/BasicROS
1. Copy file_server package to your workspace
2. Build the package
3. Install ros bridge suit dependencies
```
sudo apt-get install ros-melodic-rosbridge-server
```
4. Check package by launching
```
roslaunch file_server ros_sharp_communication.launch
```
![image](https://user-images.githubusercontent.com/55285546/126057617-56ea82dd-ca2b-41a9-a07b-8107663e8edf.png)


### Import Turtlebot3 Model to Unity
1. Download turtlebot3 package in ROS (The simulation package is optional)
> https://github.com/gmp-prem/BasicROS/tree/Turtlebot3
2. Add URDF export launch file to export Turtlebot3 model
```
cd ~/catkin_ws/src
code .
```
```
<launch>

	<include file="$(find file_server)/launch/ros_sharp_communication.launch">
		<arg name="port" value="9090" />
	</include>

	<arg name="urdf_file" default="$(find xacro)/xacro.py '$(find turtlebot3_description)/urdf/turtlebot3_burger.urdf.xacro'" />
	
	<param name="robot/name" value="Turtlebot3" />
	<param name="robot_description" command="$(arg urdf_file)" />

</launch>
```
![image](https://user-images.githubusercontent.com/55285546/126057972-a7e68ad3-9f19-499b-880f-a8cb68083b92.png)



3. Launch Turtlebot3 exporter
```
roslaunch file_server turtlebot3_description_publisher.launch
```
4. Check your Linux port
```
hostname -I
```
![image](https://user-images.githubusercontent.com/55285546/126058220-191ada18-0145-4a2b-8e6b-06b7888e0e1d.png)


4. In unity: export robot model to your scene
![image](https://user-images.githubusercontent.com/55285546/126058305-391f6405-19d1-44e6-9efd-7db90b1b91c3.png)

![image](https://user-images.githubusercontent.com/55285546/126058347-c983aa1d-495a-4681-b3ca-db6cc6a4e649.png)

![image](https://user-images.githubusercontent.com/55285546/126059012-55404993-8670-4582-a204-9ab08f3c3316.png)

### Move Turtlebot3 in AR
In unity:
1. Create empty object, add ros connector scripts and change ip address
![image](https://user-images.githubusercontent.com/55285546/126059233-40f331d9-90ac-4219-ab9a-7c9b6447361c.png)

2. Add joint states and odometry subscriber
![image](https://user-images.githubusercontent.com/55285546/126059345-53e0153b-57de-4a7e-823e-2b66df9a60d6.png)

3. Add joint_state_writer to each wheel
![image](https://user-images.githubusercontent.com/55285546/126059381-07a8169e-e253-434a-a02c-77d8b1aea953.png)

4. Drag each wheel to joint state write field in joint state subscriber and the final inspector is like this
![image](https://user-images.githubusercontent.com/55285546/126059437-d9264bf8-81ca-48be-b2f6-dee2007c4b93.png)

