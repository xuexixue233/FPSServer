#include "DbManager.h"

bool DbManager::Connect(string db, string ip, int port, string user, string pw){
    try {
        driver = get_mysql_driver_instance();
        conn = shared_ptr<Connection>(driver->connect("tcp://" + ip + ":" + to_string(port), user, pw));
        conn->setSchema(db);
        cout << "[数据库] 连接成功\n";
        return true;
    } catch (SQLException &e) {
        cerr << "[数据库] 连接失败: " << e.what() << "\n";
        return false;
    }
}

void DbManager::CheckAndReconnect() {
    try {
        if (conn->isClosed()) {
            conn->reconnect();
            cout << "[数据库] 重连成功\n";
        }
    } catch (SQLException &e) {
        cerr << "[数据库] 重连失败: " << e.what() << "\n";
    }
}

bool DbManager::IsSafeString(string str) {
    return !regex_search(str, regex(R"([-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\'])"));
}

bool DbManager::IsAccountExist(string id) {
    CheckAndReconnect();

    if (!IsSafeString(id)) {
        return false;
    }

    try {
        shared_ptr<PreparedStatement> pstmt(conn->prepareStatement("SELECT * FROM user WHERE account=?"));
        pstmt->setString(1, id);
        shared_ptr<ResultSet> res(pstmt->executeQuery());
        return res->next();
    } catch (SQLException &e) {
        cerr << "[数据库] IsAccountExist 查询失败: " << e.what() << "\n";
        return false;
    }
}

bool DbManager::Register(string id, string pw) {
    CheckAndReconnect();

    if (!IsSafeString(id) || !IsSafeString(pw)) {
        cout << "[数据库] Register 失败, id或pw不安全\n";
        return false;
    }

    if (IsAccountExist(id)) {
        cout << "[数据库] Register 失败, id已存在\n";
        return false;
    }

    try {
        shared_ptr<PreparedStatement> pstmt(conn->prepareStatement("INSERT INTO user (account, password) VALUES (?, ?)"));
        pstmt->setString(1, id);
        pstmt->setString(2, pw);
        pstmt->executeUpdate();
        return true;
    } catch (SQLException &e) {
        cerr << "[数据库] Register 失败: " << e.what() << "\n";
        return false;
    }
}

bool DbManager::CheckPassword(string id, string pw) {
    CheckAndReconnect();

    if (!IsSafeString(id) || !IsSafeString(pw)) {
        cout << "[数据库] CheckPassword 失败, id或pw不安全\n";
        return false;
    }

    try {
        shared_ptr<PreparedStatement> pstmt(conn->prepareStatement("SELECT * FROM user WHERE account=? AND password=?"));
        pstmt->setString(1, id);
        pstmt->setString(2, pw);
        shared_ptr<ResultSet> res(pstmt->executeQuery());
        return res->next();
    } catch (SQLException &e) {
        cerr << "[数据库] CheckPassword 查询失败: " << e.what() << "\n";
        return false;
    }
}