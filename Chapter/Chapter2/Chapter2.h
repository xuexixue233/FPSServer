//
// Created by wlz20 on 2024/4/27.
//

#ifndef FPSSERVER_CHAPTER2_H
#define FPSSERVER_CHAPTER2_H

#include <iostream>
#include <netinet/in.h>
#include <cstring>
#include <unistd.h>
#include <arpa/inet.h>
#include <map>

using namespace std;

class ClientState {
public:
    int socket;
    char readBuff[1024];
};

class Chapter2 {
public:
    Chapter2();

    // 监听Socket
    int listenfd;

    // 客户端Socket及状态信息
    map<int, ClientState> clients;

    //主入口
    void Main();

    // 读取Listenfd
    void ReadListenfd(int listenfd);

    // 读取Clientfd
    bool ReadClientfd(int clientfd, ClientState &state);

    ~Chapter2();
};


#endif //FPSSERVER_CHAPTER2_H
