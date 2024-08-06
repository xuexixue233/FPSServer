//
// Created by Admin on 2024/7/31.
//

#ifndef FPSSERVER_DBMANAGER_H
#define FPSSERVER_DBMANAGER_H
#include <iostream>
#include <mysql/jdbc.h>

using namespace std;
using namespace sql;

class DbManager {
public:
    bool Connect(string db, const char *ip, int port, const char *user, string pw);
    DbManager();
    ~DbManager();
private:
    unique_ptr<Connection> mysql;
};


#endif //FPSSERVER_DBMANAGER_H
