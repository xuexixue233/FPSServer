//
// Created by Admin on 2024/7/31.
//

#ifndef FPSSERVER_DBMANAGER_H
#define FPSSERVER_DBMANAGER_H
#include <string>
#include "mysql.h"

using namespace std;


class DbManager {
public:
    static bool Connect(string db, string ip, int port, string user, string pw);
};


#endif //FPSSERVER_DBMANAGER_H
