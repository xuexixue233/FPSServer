//
// Created by wlz20 on 2024/4/27.
//
#include "Chapter2.h"
using namespace std;

Chapter2::Chapter2()
{
    listenfd=0;
}

void Chapter2::Main()
{
    // 创建Socket
    listenfd = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (listenfd == -1) {
        cerr << "Failed to create socket" << endl;
        return;
    }

    // 绑定
    sockaddr_in serverAddr;
    memset(&serverAddr, 0, sizeof(serverAddr));
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_addr.s_addr = inet_addr("172.16.0.148");
    serverAddr.sin_port = htons(8888);
    if (bind(listenfd, (struct sockaddr*)&serverAddr, sizeof(serverAddr)) == -1) {
        cerr << "Failed to bind" << endl;
        close(listenfd);
        return;
    }

    // 监听
    if (listen(listenfd, 0) == -1) {
        cerr << "Failed to listen" << endl;
        close(listenfd);
        return;
    }

    cout << "[服务器]启动成功" << endl;

    // 主循环
    while (true) {
        fd_set readfds;
        FD_ZERO(&readfds);
        FD_SET(listenfd, &readfds);
        int maxfd = listenfd;

        // 将所有客户端套接字加入到fd_set中
        for (auto& kv : clients) {
            int clientfd = kv.first;
            FD_SET(clientfd, &readfds);
            if (clientfd > maxfd) {
                maxfd = clientfd;
            }
        }

        // 调用select函数检查是否有可读事件发生
        int ret = select(maxfd + 1, &readfds, NULL, NULL, NULL);
        if (ret <= 0) {
            cerr << "select() failed" << endl;
            continue;
        }

        // 检查listenfd
        if (FD_ISSET(listenfd, &readfds)) {
            ReadListenfd(listenfd);
        }

        // 检查clientfd
        for (auto& kv : clients) {
            int clientfd = kv.first;
            if (FD_ISSET(clientfd, &readfds)) {
                if (!ReadClientfd(clientfd, kv.second)) {
                    break;
                }
            }
        }
    }
}
void Chapter2::ReadListenfd(int listenfd)
{
    std::cout << "Accept" << std::endl;
    int clientfd = accept(listenfd, NULL, NULL);
    if (clientfd == -1) {
        std::cerr << "Failed to accept connection" << std::endl;
        return;
    }
    clients[clientfd] = ClientState();
    clients[clientfd].socket = clientfd;
}

bool Chapter2::ReadClientfd(int clientfd, ClientState &state)
{
    int count = recv(clientfd, state.readBuff, sizeof(state.readBuff), 0);
    if (count <= 0) {
        close(clientfd);
        clients.erase(clientfd);
        std::cout << "Socket Close" << std::endl;
        return false;
    }

    // 广播
    std::string recvStr(state.readBuff, count);
    std::cout << "Receive " << recvStr << std::endl;
    std::string sendStr = std::to_string(clientfd) + ":" + recvStr;
    send(clientfd, sendStr.c_str(), sendStr.length(), 0);

    return true;
}

Chapter2::~Chapter2() {
    listenfd=0;
    clients.clear();
}

