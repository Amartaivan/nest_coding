#include <iostream>
#include <fstream>
#include <ctime>

using namespace std;

int main(){

    ofstream file("testcase");
    srand(time(NULL));

    int n = rand() % 10000;
    file << n << endl;
    for (int i = 0; i < n; i++)
        file << rand() << ' ';

    return 0;
}