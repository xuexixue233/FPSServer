#include <iostream>
#include <cstring>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <unistd.h>

using namespace std;

class BindException : public std::exception {
public:
    BindException(const std::string& message) : message_(message) {}
    const char* what() const noexcept override {
        return message_.c_str();
    }
private:
    std::string message_;
};

int main(void)
{
    int listenfd = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (listenfd == -1) {
        std::cerr << "Failed to create socket" << std::endl;
        return 0;
    }

    sockaddr_in serverAddr;
    memset(&serverAddr, 0, sizeof(serverAddr));
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
    serverAddr.sin_port = htons(8888);

    if (bind(listenfd, (struct sockaddr*)&serverAddr, sizeof(serverAddr)) == -1) {
        // 捕获绑定错误，并抛出异常
        std::string errorMessage = "Failed to bind: ";
        errorMessage += strerror(errno); // 获取错误消息
        throw BindException(errorMessage);
    }

    if (listen(listenfd, 0) == -1) {
        std::cerr << "Failed to listen" << std::endl;
        close(listenfd);
        return 0;
    }

    cout<<"服务器启动成功"<<endl;

    while (true) {
        sockaddr_in clientAddr;
        socklen_t clientAddrLen = sizeof(clientAddr);
        int connfd = accept(listenfd, (struct sockaddr*)&clientAddr, &clientAddrLen);
        if (connfd == -1) {
            std::cerr << "Failed to accept connection" << std::endl;
            continue;
        }

        char readBuff[1024];
        int count = recv(connfd, readBuff, sizeof(readBuff), 0);
        if (count <= 0) {
            std::cerr << "Failed to receive data" << std::endl;
            close(connfd);
            continue;
        }

        std::string readStr(readBuff, count);
        send(connfd, readStr.c_str(), readStr.length(), 0);
    }
}
