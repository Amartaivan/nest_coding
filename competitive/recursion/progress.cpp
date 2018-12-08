#include <iostream>
using namespace std;

int element_a(int n, int a_1, int d){
    if (n == 1)
        return a_1;
    return element_a(n - 1, a_1, d) + d;
}
long long element_b(int n, int b_1, int q){
    if (n == 1)
        return b_1;
    return element_b(n - 1, b_1, q) * q;
}

int main(){

    // int a_1, d, n;
    // cin >> a_1 >> d >> n;

    // cout << element_a(n, a_1, d) << endl;

    int b_1, d, n;
    cin >> b_1 >> d >> n;

    cout << element_b(n, b_1, d) << endl;
    return 0;
}