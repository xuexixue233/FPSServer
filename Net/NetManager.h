//
// Created by Admin on 2024/7/31.
//

#ifndef FPSSERVER_NETMANAGER_H
#define FPSSERVER_NETMANAGER_H


#include "../Common/Singleton.h"

class NetManager: public Singleton<NetManager>{
public:
    static void StartLoop(int listenPort);
private:

};


#endif //FPSSERVER_NETMANAGER_H
