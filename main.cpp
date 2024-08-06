#include <iostream>
#include "Net/NetManager.h"
#include "Db/DbManager.h"
using namespace std;

int main() {
    if(!DbManager::Connect("fpsserver", "127.0.0.1", 3306, "root", "b5fe5bf42418724e")){
        return 0;
    }
    NetManager::StartLoop(8888);
}
