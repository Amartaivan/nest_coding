#include <iostream>

using namespace std;

int main(){

    char a[10] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    int tmp = 1;

    while (tmp != 0){
        cin >> tmp;
        a[tmp]++;
    }

    for (int i = 1; i < 10; i++)
        cout << (int)a[i] << ' ';
    return 0;
}