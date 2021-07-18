# The unity sample of ARCore and ROS implementation with ROS#


On developoing pls w8 :)

This a set of ARCore ROS samples that provides assets using ROS#  for the comunication between ROS and Mobile node.

The version of this example are
- Unity 2019
- ROS Noetic on Unbuntu 20.04

## Setting Up
### Install arcore sdk for unity
1. follow quick start guide
> https://developers.google.com/ar/develop/unity/quickstart-android

2. config custom gradle
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
4. Copy file_server package to your workspace
5. Build the package

