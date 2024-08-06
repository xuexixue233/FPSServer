//
// Created by Admin on 2024/7/31.
//
#include "DbManager.h"

DbManager::DbManager() {
    sql= mysql_init(nullptr);
}

bool DbManager::Connect(string db, const char *ip, int port, const char *user, string pw) {
//    if (!mysql_real_connect(sql, ip, user, pw, db, 0, NULL, 0)) {
//        cerr << "MySQL connection failed: " << mysql_error(conn) << std::endl;
//        return 1;
//    }
}
