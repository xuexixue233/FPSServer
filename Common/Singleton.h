//
// Created by Admin on 2024/8/7.
//

#ifndef FPSSERVER_SINGLETON_H
#define FPSSERVER_SINGLETON_H


#include <memory>
#include <mutex>
#include <iostream>
using namespace std;

template<typename T>
class Singleton
{

public:
    // 获取单实例对象
    static T& GetInstance();

    // 打印实例地址
    void Print();

private:
    // 禁止外部构造
    Singleton()=default;

    // 禁止外部析构
    ~Singleton()=default;

    // 禁止外部拷贝构造
    Singleton(const Singleton &single) = delete;

    // 禁止外部赋值操作
    const T &operator=(const Singleton &single) = delete;
};

#endif //FPSSERVER_SINGLETON_H
