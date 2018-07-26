#include <iostream>

using namespace std;

int main(){

    int n, i;
    cin >> n;
    int* a = new int[n];

    for (i = 0; i < n; i++)
        cin >> a[i];

    for (i = 0; i < n; i++)
        if (a[i] % 2 == 1)
            cout << a[i] << ' ';
    for (i = 0; i < n; i++)
        if (a[i] % 2 == 0)
            cout << a[i] << ' ';
    cout << endl;
    return 0;
}