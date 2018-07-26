#include <iostream>

using namespace std;

int main(){

    int x1, y1, x2, y2;
    cin >> x1 >> y1 >> x2 >> y2;

    if (y1 - x2 < 0)
        cout << "N" << endl;
    else
        cout << y1 - x2 << endl;

    return 0;
}