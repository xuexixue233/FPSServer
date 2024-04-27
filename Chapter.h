//
// Created by wlz20 on 2024/4/27.
//

#ifndef FPSSERVER_CHAPTER_H
#define FPSSERVER_CHAPTER_H
#include <iostream>
using namespace std;

class Chapter {
public:
    int chapterNum;
    Chapter(int num);
    void *ptr;
    void chapter2();
    void chapter3();
    void SwitchChapter(int index);
};


#endif //FPSSERVER_CHAPTER_H
