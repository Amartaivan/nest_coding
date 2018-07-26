#include <iostream>

using namespace std;

int main(){

    int n, k, s, tmp, c = 0;

    cin >> n >> k;

    for (int i = 1; i <= n; i++){
        s = 0;
        tmp = i;

        while (tmp > 0){
            s += tmp % 10;
            tmp /= 10;
        }
        if (s % k == 0)
            c++;
    }

    cout << c << endl;

    return 0;
}