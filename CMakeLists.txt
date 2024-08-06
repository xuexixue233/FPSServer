cmake_minimum_required(VERSION 3.27)
project(FPSServer)

set(CMAKE_CXX_STANDARD 17)
SET(LIB_MYSQL "libmysql.lib")

INCLUDE_DIRECTORIES(${PROJECT_SOURCE_DIR}/ExtLib/mysql/include)
LINK_DIRECTORIES(${PROJECT_SOURCE_DIR}/ExtLib/mysql/lib)

add_executable(FPSServer main.cpp
        Net/NetManager.cpp
        Net/NetManager.h
        Db/DbManager.cpp
        Db/DbManager.h)

TARGET_LINK_LIBRARIES(FPSServer
    ${LIB_MYSQL}
)
