#include <iostream>

using namespace std;

int main(){
    int n, m;
    cin >> n >> m;
    int n1 = n, m1 = m;

    while (n > 0 && m > 0){
        if (n > m)
            n = n % m;
        else
            m = m % n;
    }

    cout << n1 * m1 / (n + m) << endl;
    return 0;
}