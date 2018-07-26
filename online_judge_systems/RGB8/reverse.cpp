#include <iostream>

using namespace std;

int main(){

    int n, i;
    cin >> n;
    int* a = new int[n];

    for (i = 0; i < n; i++)
        cin >> a[i];

    for (i = n - 1; i >= 0; i--)
        if (i != 0)
            cout << a[i] << ' ';
        else
            cout << a[i];
    cout << endl;
    return 0;
}