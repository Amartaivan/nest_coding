#include <iostream>

using namespace std;

int main(){

    int n, i, j, count = 0;
    cin >> n;
    int* a = new int[n];

    for (i = 0; i < n; i++)
        cin >> a[i];

    for (i = 0; i < n - 1; i++)
        for (j = i + 1; j < n; j++)
            if (a[i] == a[j])
                count++;

    cout << count << endl;
    return 0;
}