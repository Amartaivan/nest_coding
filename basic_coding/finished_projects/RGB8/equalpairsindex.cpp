#include <iostream>

using namespace std;

int main(){

    int n, i, j, result1 = -1, result2, sf = 0;
    cin >> n;
    int* a = new int[n];

    for (i = 0; i < n; i++)
        cin >> a[i];

    for (i = 0; i < n - 1 && sf == 0; i++)
        for (j = i + 1; j < n && sf == 0; j++)
            if (a[i] == a[j])
            {
                result1 = i;
                result2 = j;
                sf = 1;
            }

    if (result1 == -1)
        cout << "0 0" << endl;
    else
        cout << result1 + 1 << ' ' << result2 + 1 << endl;
    return 0;
}