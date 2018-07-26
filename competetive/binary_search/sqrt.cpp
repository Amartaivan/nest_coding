#include <iostream>

using namespace std;

int main(){

    int n;
    cin >> n;

    long s = 1, f = n, m;
    while (s < f){
        m = (s + f + 1) / 2;

        if (m * m > n)
            f = m - 1;
        else
            s = m;
    }

    cout << s << endl;
}