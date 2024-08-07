//
// Created by Admin on 2024/8/7.
//

#include "Singleton.h"

template<typename T>
T &Singleton<T>::GetInstance() {
    static T Singleton;
    return Singleton;
}

template<typename T>
void Singleton<T>::Print() {
    cout << "我的实例内存地址是:" << this << endl;
}
