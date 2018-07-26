#include <iostream>

using namespace std;

int main(){

    int n, i;
    cin >> n;
    int* a = new int[n];

    for (i = 0; i < n; i++)
        cin >> a[i];

    for (i = 1; i <= n; i++)
        if (i % 2 == 1)
            cout << a[i - 1] << ' ';
    for (i = 1; i <= n; i++)
        if (i % 2 == 0)
            cout << a[i - 1] << ' ';
    cout << endl;
    return 0;
}