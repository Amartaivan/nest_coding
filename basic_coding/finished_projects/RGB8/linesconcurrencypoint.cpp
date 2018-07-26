#include <iostream>

using namespace std;

int main(){

    int x1, y1, x2, y2, n, i;
    cin >> n >> x1 >> y1;

    for (i = 1; i < n; i++){
        cin >> x2 >> y2;
        if (y1 - x2 < 0)
            i = n + 2;
        else
        {
            x1 = x2;
            y1 = y1;
        }
    }

    if (i = n + 2)
        cout << y1 - x1 << endl;
    else
        cout << "0" << endl;
    return 0;
}