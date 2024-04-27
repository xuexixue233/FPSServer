cmake_minimum_required(VERSION 3.28)
project(FPSServer)

set(CMAKE_CXX_STANDARD 17)

add_executable(FPSServer main.cpp
        Chapter.cpp
        Chapter.h
        Chapter/Chapter2/Chapter2.cpp
        Chapter/Chapter2/Chapter2.h)
