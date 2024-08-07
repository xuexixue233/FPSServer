//
// Created by Admin on 2024/7/31.
//

#ifndef FPSSERVER_DBMANAGER_H
#define FPSSERVER_DBMANAGER_H
#include <iostream>
#include <mysql/jdbc.h>
#include <iostream>
#include <regex>

using namespace std;
using namespace sql;
using namespace mysql;

class DbManager {
public:
    static DbManager * getInstance();
    DbManager();
    ~DbManager();

    bool Connect(string db, string ip, int port, string user, string pw);
    bool IsAccountExist(string id);
    bool Register(string id, string pw);
    bool CheckPassword(string id, string pw);

private:
    void CheckAndReconnect();
    bool IsSafeString(string str);
    static DbManager *instance ;

    shared_ptr<Connection> conn;
    MySQL_Driver* driver;
    string db;
    string ip;
    int port;
    string user;
    string pw;
};


#endif //FPSSERVER_DBMANAGER_H
