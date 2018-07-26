#include <iostream>

using namespace std;

int main(){

    int n, m, lcm, sf = 0;
    cin >> n >> m;

    if (n > m)
        lcm = n;
    else
        lcm = m;
    for (; sf == 0; lcm++)
        if (lcm % n == 0 && lcm % m == 0)
            sf = 1;
    
    lcm--;
    cout << lcm << endl;//cout << --lcm << endl;
    return 0;
}