//
// Created by Admin on 2024/7/31.
//

#ifndef FPSSERVER_DBMANAGER_H
#define FPSSERVER_DBMANAGER_H
#include <string>
#include <iostream>
#include "mysql.h"
#include <regex>

using namespace std;


class DbManager {
public:

    bool Connect(string db, const char *ip, int port, const char *user, string pw);
    DbManager();
    ~DbManager();
private:
    MYSQL *sql;
};


#endif //FPSSERVER_DBMANAGER_H
