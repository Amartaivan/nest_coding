#include <iostream>

using namespace std;

int main(){

    int n, m, lcm, add, div, sf = 0;
    cin >> n >> m;

    if (n > m){
        lcm = n;
        add = n;
        div = m;
    }
    else{
        lcm = m;
        add = m;
        div = n;
    }
    for (; sf == 0; lcm += add)
        if (lcm % div == 0)
            sf = 1;
    
    lcm -= add;
    cout << lcm << endl;//cout << --lcm << endl;
    return 0;
}