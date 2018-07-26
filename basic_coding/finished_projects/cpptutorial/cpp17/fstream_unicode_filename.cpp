#include <iostream>
#include <fstream>
using namespace std;

int main(){

    wofstream out;
    ofstream(L"хаха") << u8"yay!" << endl;
    return 0;
}