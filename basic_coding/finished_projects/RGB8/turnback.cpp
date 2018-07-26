#include <iostream>

using namespace std;

int main(){

    int n, i, t;
    cin >> n >> t;
    t %= n;
    t = n - t;

    for (i = 0; i < n; i++){
        t++;
        if (t % n == 0) 
            cout << n << ' ';
        else 
            cout << t % n << ' ';
    }

    return 0;
}