cmake_minimum_required(VERSION 3.27)
project(FPSServer)

set(CMAKE_CXX_STANDARD 17)

# 设置 MySQL Connector/C++ 的路径
set(CPP_CONN_INCLUDE_DIR "${PROJECT_SOURCE_DIR}/ExtLib/mysql/include")
set(CPP_CONN_LIB_DIR "${PROJECT_SOURCE_DIR}/ExtLib/mysql/lib64")

# 包含 MySQL Connector/C++ 的头文件
include_directories(${CPP_CONN_INCLUDE_DIR})

add_executable(FPSServer main.cpp
        Net/NetManager.cpp
        Net/NetManager.h
        Db/DbManager.cpp
        Db/DbManager.h
        Common/Singleton.cpp
        Common/Singleton.h)

# 链接 MySQL Connector/C++ 库
target_link_libraries(FPSServer
        ${CPP_CONN_LIB_DIR}/mysqlcppconn.lib
        ${CPP_CONN_LIB_DIR}/mysqlcppconn9.lib
)
