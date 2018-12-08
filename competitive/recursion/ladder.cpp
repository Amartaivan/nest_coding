#include <iostream>

using namespace std;

int calculate(int n){
    if (n < 3)
        return 1;
    if (n == 3)
        return 2;

    return calculate(n - 1) + calculate(n - 3);
}

int main(){

    int n;
    cin >> n;

    cout << calculate(n) << endl;
    return 0;
}